using System;
using Microsoft.IdentityModel.Tokens;

namespace CodingDays.UserApi.Models.Config;
public record JwtHolder
(
    string PrivateKey
)
{
    private SymmetricSecurityKey? _key;
    public SymmetricSecurityKey Key
    {
        get
        {
            if (_key is null)
                _key = new SymmetricSecurityKey(Convert.FromBase64String(PrivateKey));

            return _key;
        }
    } 
}
