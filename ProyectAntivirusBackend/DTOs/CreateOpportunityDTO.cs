namespace ProyectAntivirusBackend.DTOs
{
    public class CreateOpportunityDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Sector { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public int OpportunityTypes { get; set; }
        public int Institution { get; set; }
        public string Status { get; set; } = "abierta";
    }
}