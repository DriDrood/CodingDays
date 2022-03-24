using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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
[Route("api/[controller]")]
public class SessionController : ControllerBase
{
    private static Dictionary<Guid, string> _userTokens = new Dictionary<Guid, string>();
    
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

    [HttpGet("[action]")]
    public ActionResult LoginGui()
    {
        return Redirect("/");
    }

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
            Email = param.Email,
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, param.Password);

        _db.Add(user);
        await _db.SaveChangesAsync();

        return new RegisterResp(user.Id);
    }

    [HttpPost("[action]")]
    public async Task ForgotPassword(ForgotPasswordReq param)
    {
        User user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == param.Name)
            ?? throw new UsageException("Uživatel nenalezen");
        
        // generate token
        Random random = new Random();
        byte[] buffer = new byte[24];
        random.NextBytes(buffer);
        string token = Convert.ToBase64String(buffer);
        _userTokens[user.Id] = token;

        // send mail
        HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Post, "https://api.sendinblue.com/v3/smtp/email");
        message.Headers.Add("Accept", "application/json");
        message.Headers.Add("api-key", _secretHolder.MailApiKey);
        object content = new
        {
            sender = new
            {
                name = "Users - RangerHub.org",
                email = "users@rangerhub.org",
            },
            to = new object[]
            {
                new
                {
                    name = user.UserName,
                    email = user.Email,
                }
            },
            subject = "Reset password",
            htmlContent = $"<html><head></head><body><p>Hello,</p><p>Use this link to reset your password: <a href='http://users.rangerhub.org/resetPassword?token={token}'>Reset password</a></p></body></html>"
        };
        message.Content = new StringContent(JsonSerializer.Serialize(content), Encoding.UTF8, "application/json");
        
        using (HttpClient client = new HttpClient())
        {
            HttpResponseMessage result = await client.SendAsync(message);

            if (!result.IsSuccessStatusCode)
            {
                string reason = await result.Content.ReadAsStringAsync();
                throw new UsageException(reason);
            }
        }
    }

    [HttpPost("[action]")]
    public async Task ResetPassword(ResetPasswordReq param)
    {
        User user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == param.Name)
            ?? throw new UsageException("Uživatel nenalezen");

        if (!_userTokens.TryGetValue(user.Id, out string? token) || token != param.Token)
            throw new UsageException("Neplatný token");

        user.PasswordHash = _passwordHasher.HashPassword(user, param.NewPassword);
        await _db.SaveChangesAsync();
    }
}
