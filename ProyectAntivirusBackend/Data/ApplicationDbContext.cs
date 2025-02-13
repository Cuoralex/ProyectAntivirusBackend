using Microsoft.EntityFrameworkCore;
using ProyectAntivirusBackend.Models;
namespace ProyectAntivirusBackend.Data
{
 public class ApplicationDbContext : DbContext
 {
 public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
: base(options) { }
 public DbSet<OpportunityType> OpportunityTypes { get; set; }
 protected override void OnModelCreating(ModelBuilder modelBuilder)
 {
 modelBuilder.Entity<OpportunityType>().ToTable("opportunity_types");
 
 base.OnModelCreating(modelBuilder);
 }
 }
}
