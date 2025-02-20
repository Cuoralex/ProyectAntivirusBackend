using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Agregar esto

namespace ProyectAntivirusBackend.Models
{
    [Table("opportunity_types")]
    public class OpportunityType
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("Name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("Description")]
        public string? Description { get; set; }

        [JsonIgnore] // Evita que Swagger y la API lo incluyan en los JSON
        public ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
    }
}
