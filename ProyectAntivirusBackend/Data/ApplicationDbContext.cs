using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
<<<<<<< HEAD
<<<<<<< HEAD
        public DbSet<User> Users { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Service> Services { get; set; }


=======
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
<<<<<<< HEAD
        public DbSet<OpportunityType> OpportunityTypes { get; set; }
        public DbSet<Request> Requests { get; set; }

        // Eliminamos el constructor incorrecto
        // public AppDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OpportunityType>().ToTable("opportunity_types");
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("users", t => t.ExcludeFromMigrations());
        }
        
>>>>>>> origin/DevCuoralex
=======
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public required DbSet<User> Users { get; set; }

        public required DbSet<ServiceType> ServicesTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().ToTable("services_types");
        }
>>>>>>> origin/DevDavalejo
    }
}
=======
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
>>>>>>> origin/DevGeny
