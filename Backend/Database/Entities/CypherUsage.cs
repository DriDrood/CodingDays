using System;
using System.ComponentModel.DataAnnotations;

namespace CodingDays.Database.Entities;
public class CypherUsage
{
    public CypherUsage()
    {
        CypherId = null!;
        Cypher = null!;
        Team = null!;
        Hint = null!;
    }

    [StringLength(40)]
    public string CypherId { get; set; }
    public Cypher Cypher { get; set; }
    public Guid TeamId { get; set; }
    public Team Team { get; set; }
    public Guid HintId { get; set; }
    public Hint Hint { get; set; }
}
