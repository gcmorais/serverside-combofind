using combofind.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace combofind.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Collection> Collection { get; set; }
        public DbSet<GunEntity> Guns { get; set; }
    }
}
