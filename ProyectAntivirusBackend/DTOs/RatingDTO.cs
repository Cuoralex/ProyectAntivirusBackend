public class RatingDTO
{
    public int Score { get; set; }  // ✅ Debe ser int
    public required string Comment { get; set; }  // ✅ Debe ser string
}
