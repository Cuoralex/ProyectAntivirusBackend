namespace ProyectAntivirusBackend.DTOs
{
    public class OpportunityDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Opportunity_Types_id { get; set; }
        public int Sectors_id { get; set; }
        public int Localities_id { get; set; }
        public string Requirements { get; set; } = string.Empty;
        public string Benefits { get; set; } = string.Empty;
        public required string Modality { get; set; }
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;
        public DateTime ExpirationDate { get; set; } = DateTime.UtcNow;
        public int Institutions_id { get; set; }
        public string Status { get; set; } = "abierta"; // Estado por defecto: "abierta"

    }
}