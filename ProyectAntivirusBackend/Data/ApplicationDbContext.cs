// Data/ApplicationDbContext.cs
using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<AuthUser> AuthUsers { get; set; }
        public DbSet<Opportunity> Opportunities { get; set; } // Nueva línea

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno a uno entre User y AuthUser
            modelBuilder.Entity<User>()
                .HasOne(u => u.AuthUser)
                .WithOne(a => a.User)
                .HasForeignKey<AuthUser>(a => a.UserId);

            // Índice único para el correo
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            // Índice para el título de la oportunidad
            modelBuilder.Entity<Opportunity>()
                .HasIndex(o => o.Title)
                .IsUnique();
        }
    }
}