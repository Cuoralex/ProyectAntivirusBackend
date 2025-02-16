using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
<<<<<<< HEAD
        public DbSet<User> Users { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Service> Services { get; set; }


=======
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
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
    }
}
