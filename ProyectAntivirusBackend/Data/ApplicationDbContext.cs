using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;

namespace ProyectAntivirusBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public required DbSet<User> Users { get; set; }

        public required DbSet<ServiceType> ServicesTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServiceType>().ToTable("services_types");
        }
    }
}
