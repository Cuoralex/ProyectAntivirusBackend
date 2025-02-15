using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Column("nombre")]
        public string Name { get; set; } = string.Empty;

        [Column("correo")]
        public string Email { get; set; } = string.Empty;

        [Column("telefono")]
        public string? Phone { get; set; }

        [Column("rol")]
        public string Role { get; set; } = "estudiante";

        [Column("fecha_registro")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Column("activo")]
        public bool IsActive { get; set; } = true;

        // Relación uno a uno con Profile
        public virtual Profile? Profile { get; set; }
    }
}
