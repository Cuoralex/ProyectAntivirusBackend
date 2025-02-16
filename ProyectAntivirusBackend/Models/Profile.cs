using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("profiles")]
    public class Profile
    {
        [Key]
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("User")]
        [Column("usuario_id")]
        public Guid UserId { get; set; }

        [Column("preferencias")]
        public string Preferences { get; set; } = "{}";

        [Column("biografia")]
        public string Biography { get; set; } = string.Empty;

        [Column("foto_perfil")]
        public string ProfilePicture { get; set; } = string.Empty;

        // Relación con User
        public virtual required User User { get; set; }
    }
}
