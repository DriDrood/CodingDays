using CodingDays.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodingDays.Database
{
    public class DB : DbContext
    {
        public DB(DbContextOptions<DB> options) : base(options)
        { }
        public DB() : this(new DbContextOptions<DB>())
        { }

        public DbSet<Registration> Registrations => Set<Registration>();
    }
}