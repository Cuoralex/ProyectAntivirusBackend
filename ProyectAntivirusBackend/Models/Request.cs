// Models/Request.cs
public class Request
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int OpportunityId { get; set; }
    public string State { get; set; }
    public DateTime RequestDate { get; set; }
}