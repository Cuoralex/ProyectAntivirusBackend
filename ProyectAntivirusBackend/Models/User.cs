using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("users")] // Nombre de la tabla en PostgreSQL
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("email")]
        public string Email { get; set; } = string.Empty;

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("role")]
        public string Role { get; set; } = "study"; // Valor por defecto

        [Column("Registration_Date")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Column("Isactive")]
        public bool IsActive { get; set; } = true;

        // Relación con auth_users
        public AuthUser AuthUser { get; set; }
        
        public Profile Profile { get; set; } = null!;  
    }
}
