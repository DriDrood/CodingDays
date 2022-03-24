using System;
using CodingDays.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.Database;
public class DB : DbContext
{
    public DB(DbContextOptions<DB> options) : base(options)
    { }
    public DB() : this(new DbContextOptions<DB>())
    { }

    public DbSet<Cypher> Cyphers => Set<Cypher>();
    public DbSet<CypherUsage> CypherUsages => Set<CypherUsage>();
    public DbSet<Hint> Hints => Set<Hint>();
    public DbSet<Registration> Registrations => Set<Registration>();
    public DbSet<Team> Teams => Set<Team>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // CypherUsage
        modelBuilder.Entity<CypherUsage>()
            .HasKey(cu => new { cu.TeamId, cu.CypherId });

        // default hint
        modelBuilder.Entity<Hint>()
            .HasData(new Hint { Id = Guid.Parse("00000000-0000-0000-0000-000000000001"), Text = "Další nápovědu dostanete osobně. Tento kód můžete zadat v dalším kroku znovu" });
    }
}
