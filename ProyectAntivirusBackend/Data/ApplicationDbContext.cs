// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; } // Nueva línea

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno a uno entre User y Profile
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

            // ... otras configuraciones ...
        }
    }
}