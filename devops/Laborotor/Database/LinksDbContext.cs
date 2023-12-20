using Laborotor.Database.Entity;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace Laborotor.Database
{
    public class LinksDbContext : DbContext
    {
        public DbSet<Link> Links { get; set; }

        public LinksDbContext(DbContextOptions<LinksDbContext> options) : base(options)
        {
            Database.MigrateAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Link>().Property(el => el.Status).HasDefaultValue(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}
