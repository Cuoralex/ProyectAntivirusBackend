using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    public class Institution
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }  = string.Empty;

        [Required]
        [Column("image")]
        public string Image { get; set; } = string.Empty;

        [Column("information")]
        public string Information { get; set; } = string.Empty;
    }
}