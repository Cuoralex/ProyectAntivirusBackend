using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("service_types")] 
    public class ServiceType
    {
        [Key] 
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("name")]
        [StringLength(30)]
        public required string Name { get; set; }

        [Column("description")]
        public required string Description { get; set; }
    }
}
