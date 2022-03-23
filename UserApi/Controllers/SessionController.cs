using System;
using System.Threading.Tasks;
using CodingDays.UserApi.Database;
using CodingDays.UserApi.Database.Entities;
using CodingDays.UserApi.Exceptions;
using CodingDays.UserApi.Models.Config;
using CodingDays.UserApi.Models.Dto.Session;
using CodingDays.UserApi.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.UserApi.Controllers;
[ApiController]
[Route("[controller]")]
public class SessionController : ControllerBase
{
    public SessionController(DB db, JwtHandler jwtHandler, SecretHolder secretHolder)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<User>();
        _jwtHandler = jwtHandler;
        _secretHolder = secretHolder;
    }

    private readonly DB _db;
    private readonly PasswordHasher<User> _passwordHasher;
    private readonly JwtHandler _jwtHandler;
    private readonly SecretHolder _secretHolder;

    [HttpPost("[action]")]
    public async Task<LoginResp> Login(LoginReq param)
    {
        User user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == param.Name)
            ?? throw new UsageException("Uživatel nenalezen");

        PasswordVerificationResult verificationResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, param.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
            throw new UsageException("Chybné heslo");

        string token = _jwtHandler.GenerateToken(user);

        return new LoginResp(token);
    }

    [HttpPost("[action]")]
    public async Task<RegisterResp> Register(RegisterReq param)
    {
        _secretHolder.ValidateSecret(param.Secret);

        User user = new User
        {
            Id = Guid.NewGuid(),
            UserName = param.Name,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, param.Password);

        _db.Add(user);
        await _db.SaveChangesAsync();

        return new RegisterResp(user.Id);
    }
}
