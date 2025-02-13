// Models/User.cs
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("users")] // Nombre de la tabla en PostgreSQL
    public class User
    {
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Column("nombre")]
        public string Name { get; set; } = string.Empty;

        [Column("correo")]
        public string Email { get; set; } = string.Empty;

        [Column("telefono")]
        public string? Phone { get; set; }

        [Column("rol")]
        public string Role { get; set; } = "estudiante"; // Valor por defecto

        [Column("fecha_registro")]
        public DateTime RegistrationDate { get; set; } = DateTime.UtcNow;

        [Column("activo")]
        public bool IsActive { get; set; } = true;
    }
}