using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("profiles")] // Nombre de la tabla en PostgreSQL
    public class Profile
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("preferences")]
        public string? Preferences { get; set; }

        [Column("biography")]
        public string? Biography { get; set; }

        [Column("profile_image")]
        public string? ProfileImage { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
