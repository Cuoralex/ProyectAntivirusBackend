namespace ProyectAntivirusBackend.DTOs
{
    public class CreateOpportunityDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Sector { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public DateTime ExpirationDate { get; set; }
        public string Institution { get; set; } = string.Empty;
        public string Status { get; set; } = "abierta";
    }
}