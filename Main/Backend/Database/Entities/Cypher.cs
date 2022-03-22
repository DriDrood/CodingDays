using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CodingDays.Database.Entities;
public class Cypher
{
    public Cypher()
    {
        // created by EF - always has a value
        // just to ignore warning
        Id = null!;
        Result = null!;
    }
    public Cypher(string id, string result)
    {
        Id = id;
        Result = result;
    }

    /// <summary>
    ///   Hashed cypher result
    /// </summary>
    [StringLength(40)]
    public string Id { get; set; }

    /// <summary>
    ///  just for verification
    /// </summary>
    [StringLength(8)]
    public string Result { get; set; }

    public ICollection<CypherUsage> CypherUsages { get; set; } = new HashSet<CypherUsage>();
}
