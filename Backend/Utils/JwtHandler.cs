using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using CodingDays.Database.Entities;
using CodingDays.Models.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CodingDays.Utils;

public class JwtHandler
{
    public JwtHandler(JwtHolder config)
    {
        _config = config;
        _options = new JwtBearerOptions();
    }

    private readonly JwtHolder _config;
    private readonly JwtBearerOptions _options;

    public string GenerateToken(Team team)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = null,
            Audience = null,
            IssuedAt = DateTime.UtcNow,
            NotBefore = DateTime.UtcNow,
            Expires = DateTime.UtcNow.AddDays(1),
            Subject = new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, team.UserName),
            }),
            SigningCredentials = new SigningCredentials(_config.Key, SecurityAlgorithms.HmacSha256Signature)
        };
        var jwtTokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtTokenHandler.CreateJwtSecurityToken(tokenDescriptor);
        var token = jwtTokenHandler.WriteToken(jwtToken);

        return token;
    }

    // public bool ValidateToken(string token)
    // {
    //     List<Exception>? validationFailures = null;
    //     SecurityToken validatedToken;
    //     foreach (var validator in _options.SecurityTokenValidators)
    //     {
    //         if (validator.CanReadToken(token))
    //         {
    //             ClaimsPrincipal principal;
    //             try
    //             {
    //                 principal = validator.ValidateToken(token, _options.TokenValidationParameters, out validatedToken);
    //             }
    //             catch (Exception ex)
    //             {
    //                 // Refresh the configuration for exceptions that may be caused by key rollovers. The user can also request a refresh in the event.
    //                 if (_options.RefreshOnIssuerKeyNotFound && _options.ConfigurationManager != null
    //                     && ex is SecurityTokenSignatureKeyNotFoundException)
    //                 {
    //                     _options.ConfigurationManager.RequestRefresh();
    //                 }

    //                 if (validationFailures == null)
    //                     validationFailures = new List<Exception>(1);
    //                 validationFailures.Add(ex);
    //                 continue;
    //             }
    //             return true;
    //         }
    //     }
    //     return false;
    // }
}
