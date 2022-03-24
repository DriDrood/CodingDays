using CodingDays.UserApi.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.UserApi.Database;
public class DB : DbContext
{
    public DB(DbContextOptions<DB> options) : base(options)
    { }
    public DB() : this(new DbContextOptions<DB>())
    { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .Property(x => x.UserName).HasMaxLength(50).IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.NormalizedUserName).HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(x => x.Email).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<User>()
            .Property(x => x.NormalizedEmail).HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(x => x.PasswordHash).HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(x => x.SecurityStamp).HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(x => x.ConcurrencyStamp).HasMaxLength(100);
        modelBuilder.Entity<User>()
            .Property(x => x.PhoneNumber).HasMaxLength(20);
    }
}
