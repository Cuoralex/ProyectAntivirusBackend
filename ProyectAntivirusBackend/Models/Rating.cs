public class Rating
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OpportunityId { get; set; }
    public  required int Score { get; set; }  // Puntuación del 1 al 5
    public required string Comment { get; set; } // Justificación del usuario
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
