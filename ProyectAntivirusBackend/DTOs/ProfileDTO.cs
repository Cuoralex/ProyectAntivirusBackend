namespace ProyectAntivirusBackend.DTOs
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Preferences { get; set; } = "{}";
        public string Biography { get; set; } = string.Empty;
        public string ProfilePicture { get; set; } = string.Empty;
    }
}