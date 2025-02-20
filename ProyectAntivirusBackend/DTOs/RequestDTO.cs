namespace ProyectAntivirusBackend.DTOs
{
    public class RequestDTO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OpportunityId { get; set; }
        public required string State { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
