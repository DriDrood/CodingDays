using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingDays.Database.Entities;
public class Team
{
    public Team()
    {
        // generate random base64 string - 8 chars
        Random random = new Random();
        byte[] buffer = new byte[6];
        random.NextBytes(buffer);
        Name = Convert.ToBase64String(buffer);
    }

    public Guid Id { get; set; } = Guid.NewGuid();

    [StringLength(50)]
    public string Name { get; set; }
    public ESteps CurrentStep { get; set; }
    
    public ICollection<CypherUsage> CypherUsages { get; set; } = new HashSet<CypherUsage>();
}