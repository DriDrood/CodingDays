using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingDays.Database;
using CodingDays.Database.Entities;
using CodingDays.Exceptions;
using CodingDays.Models.Config;
using CodingDays.Models.Dto.Team;
using CodingDays.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.Controllers;
public class TeamController : ControllerBase
{
    public TeamController(DB db, JwtHandler jwtHandler, SecretHolder secretHolder)
    {
        _db = db;
        _jwtHandler = jwtHandler;
        _passwordHasher = new PasswordHasher<Team>();
        _secretHolder = secretHolder;
    }

    private readonly DB _db;
    private readonly JwtHandler _jwtHandler;
    private readonly PasswordHasher<Team> _passwordHasher;
    private readonly SecretHolder _secretHolder;

    public async Task<LoginResp> Login([FromBody] LoginReq param)
    {
        Team team = await _db.Teams.FirstOrDefaultAsync(t => t.UserName == param.Name)
            ?? throw new UsageException("Team nenalezen");

        PasswordVerificationResult result = _passwordHasher.VerifyHashedPassword(team, team.PasswordHash, param.Password);
        if (result == PasswordVerificationResult.Failed)
            throw new UsageException("Špatné heslo");

        string token = _jwtHandler.GenerateToken(team);

        return new LoginResp(token);
    }

    public async Task<RegisterResp> Register([FromBody] RegisterReq param)
    {
        _secretHolder.ValidateSecret(param.Secret);

        Guid id = param.Id ?? Guid.NewGuid();
        Team team = new Team
        {
            Id = id,
            UserName = id.ToString().Substring(32),
        };
        team.PasswordHash = _passwordHasher.HashPassword(team, param.Password);

        _db.Add(team);
        await _db.SaveChangesAsync();

        return new RegisterResp(team.Id, team.UserName);
    }

    public async Task<GetResp> Get()
    {
        Team[] teams = await _db.Teams
            .Include(t => t.Registrations)
            .ToArrayAsync();

        Dictionary<Guid, GetRespTeam> response = new Dictionary<Guid, GetRespTeam>();
        foreach (Team team in teams)
        {
            string filePath = System.IO.Path.Combine(".", "Data", $"Team-{team.UserName}", $"{team.UserName}.txt");
            bool finished = System.IO.File.Exists(filePath);
            string[] members = new string[0];

            if (finished)
            {
                DateTime[] birthDates = System.IO.File.ReadAllLines(filePath)
                    .Select(l => DateTime.TryParse(l, out DateTime birthDate) ? birthDate : DateTime.UtcNow)
                    .ToArray();
                members = birthDates
                    .Select(bd => team.Registrations.FirstOrDefault(r => r.Birth == bd))
                    .Where(mn => mn is not null)
                    .Select(mn => $"{mn!.Name} {mn.Surname}")
                    .ToArray();
            }

            GetRespTeam teamResp = new GetRespTeam(team.UserName, finished, members);
            response[team.Id] = teamResp;
        }

        return new GetResp(response);
    }
}
