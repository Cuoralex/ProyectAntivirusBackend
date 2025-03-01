namespace ProyectAntivirusBackend.DTOs
{
    public class CreateOpportunityDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public required int SectorsId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public required int OpportunityTypesId { get; set; }
        public required int InstitutionsId { get; set; }
        public string Status { get; set; } = "abierta";
    }
}