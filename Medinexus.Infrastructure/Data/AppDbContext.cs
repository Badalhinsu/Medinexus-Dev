using Medinexus.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Medinexus.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserMaster> UserMasters { get; set; }
        public DbSet<ChemistMaster> ChemistMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserMaster>()
                        .HasIndex(u => u.Email)
                        .IsUnique();
        }
    }
}
