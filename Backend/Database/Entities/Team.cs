using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace CodingDays.Database.Entities;
public class Team : IdentityUser<Guid>
{
    public Team()
    {
        // generate random base64 string - 8 chars
        Random random = new Random();
        byte[] buffer = new byte[6];
        random.NextBytes(buffer);
        UserName = Convert.ToBase64String(buffer);
    }

    public ESteps CurrentStep { get; set; }

    public ICollection<CypherUsage> CypherUsages { get; set; } = new HashSet<CypherUsage>();
    public ICollection<Registration> Registrations { get; set; } = new HashSet<Registration>();
}