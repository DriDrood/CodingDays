using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingDays.Database.Entities;
public class Hint
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public ESteps Step { get; set; }
    public byte Order { get; set; }
    public string? Text { get; set; }
    [StringLength(255)]
    public string? ImageUrl { get; set; }

    public ICollection<CypherUsage> CypherUsages { get; set; } = new HashSet<CypherUsage>();
}
