using System.ComponentModel.DataAnnotations;

namespace ProyectAntivirusBackend.DTOs
{
    public class CreateOpportunityDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Required]
        public int SectorId { get; set; }
        [Required]
        public int OpportunityTypeId { get; set; }
        public string? Location { get; set; }
        public string? Requirements { get; set; }
        public string? Benefits { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        [Required]
        public int InstitutionId { get; set; }
    }
}