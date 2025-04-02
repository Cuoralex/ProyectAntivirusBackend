using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{

    public class Rating
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [ForeignKey("User")]
        [Column("user_id")]
        public int UserId { get; set; }

        [ForeignKey("Opportunity")]
        [Column("opportunities_id")]
        public int OpportunityId { get; set; }
    [   Required]
        [Column("modality")]
        public required int Score { get; set; }
        [Required]
        [Column("modality")]
        public required string Comment { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
