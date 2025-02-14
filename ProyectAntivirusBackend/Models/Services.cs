using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("is_active")]
        public bool IsActive { get; set; } = true;

        [Column("service_type_id")]
        public int ServiceTypeId { get; set; }

        [Column("title")]
        public required string Title { get; set; }

        [Column("description")]
        public required string Description { get; set; }

        [Column("image")]
        public required string Image { get; set; }
    }
}
