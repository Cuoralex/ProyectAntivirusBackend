// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }

        public DbSet<Institution> Institutions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Institution>().ToTable("institutions");
            base.OnModelCreating(modelBuilder);
        }
    }
}