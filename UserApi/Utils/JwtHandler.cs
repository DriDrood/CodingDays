using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodingDays.UserApi.Database.Entities;
using CodingDays.UserApi.Models.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CodingDays.UserApi.Utils;
public class JwtHandler
{
    public JwtHandler(JwtHolder holder)
    {
        _holder = holder;
        _options = new JwtBearerOptions();
        _jwtTokenHandler = new JwtSecurityTokenHandler();
    }

    private readonly JwtHolder _holder;
    private readonly JwtBearerOptions _options;
    private readonly JwtSecurityTokenHandler _jwtTokenHandler;

    public string GenerateToken(User user)
    {
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = null,
            Audience = null,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.UserName),
            }),
            SigningCredentials = new SigningCredentials(_holder.Key, SecurityAlgorithms.HmacSha256Signature)
        };
        JwtSecurityToken jwtToken = _jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        string token = _jwtTokenHandler.WriteToken(jwtToken);

        return token;
    }

    public Guid? ReadIdFromToken(string? bearer)
    {
        if (bearer is null)
            return null;

        string token = bearer.Substring("Bearer ".Length);
        ClaimsPrincipal claims = _jwtTokenHandler.ValidateToken(token, GetTokenParams(_holder), out SecurityToken _);

        string? idString = claims.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(idString, out Guid id)
            ? id
            : null;
    }

    public static TokenValidationParameters GetTokenParams(JwtHolder jwtHolder)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = jwtHolder.Key,
        };
    }
}
