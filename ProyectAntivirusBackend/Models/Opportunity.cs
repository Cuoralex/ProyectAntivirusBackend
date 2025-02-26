using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("opportunities")]
    public class Opportunity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [ForeignKey("OpportunityType")]
        [Column("opportunity_type_id")]
        public int OpportunityTypeId { get; set; }
        public required OpportunityType OpportunityType { get; set; }

        [Required]
        [ForeignKey("Sector")]
        [Column("sector_id")]
        public int SectorId { get; set; }
        public required Sector Sector { get; set; }

        [Column("location")]
        public string? Location { get; set; }

        [Column("requirements")]
        public string? Requirements { get; set; }

        [Column("benefits")]
        public string? Benefits { get; set; }

        [Required]
        [Column("publication_date")]
        public DateTime PublicationDate { get; set; }

        [Required]
        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [ForeignKey("Institution")]
        [Column("institution_id")]
        public int InstitutionId { get; set; }
        public required Institution Institution { get; set; }
    }
}
