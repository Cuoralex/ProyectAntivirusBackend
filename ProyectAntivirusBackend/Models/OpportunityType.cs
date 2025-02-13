using System.ComponentModel.DataAnnotations;

namespace ProyectAntivirusBackend.Models
{
    public class OpportunityType
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(255)]
        public string? Description { get; set; }
    }
}
