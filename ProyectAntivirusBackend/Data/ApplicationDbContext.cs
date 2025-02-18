using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ServiceType> ServicesTypes { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<OpportunityType> OpportunityTypes { get; set; }

        public DbSet<AuthUser> AuthUsers { get; set; }
        
        public DbSet<Opportunity> Opportunity { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Relación uno a uno entre User y Profile
            modelBuilder.Entity<User>()
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>(p => p.UserId);

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

            // ... otras configuraciones ...
        }
    }
}
