using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ProyectAntivirusBackend.Models
{
    [Table("opportunity_types")]
    public class OpportunityType
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        [Column("description")]
        public string? Description { get; set; }

        [JsonIgnore]
        public ICollection<Opportunity> Opportunities { get; set; } = new List<Opportunity>();
    }
}
