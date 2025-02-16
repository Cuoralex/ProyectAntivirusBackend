// Models/Request.cs
public class Request
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OpportunityId { get; set; }
    public string State { get; set; } = string.Empty;
    public DateTime RequestDate { get; set; }
}