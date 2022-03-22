using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodingDays.Database;
using CodingDays.Database.Entities;
using CodingDays.Models.Dto.Team;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.Controllers;
public class TeamController : ControllerBase
{
    public TeamController(DB db)
    {
        _db = db;
    }

    private DB _db;

    public async Task<GetResp> Get()
    {
        Team[] teams = await _db.Teams
            .Include(t => t.Registrations)
            .ToArrayAsync();
        
        Dictionary<Guid, GetRespTeam> response = new Dictionary<Guid, GetRespTeam>();
        foreach(Team team in teams)
        {
            string filePath = System.IO.Path.Combine(".", "Data", $"Team-{team.Id}", $"{team.Name}.txt");
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

            GetRespTeam teamResp = new GetRespTeam(team.Name, finished, members);
            response[team.Id] = teamResp;
        }

        return new GetResp(response);
    }
}
