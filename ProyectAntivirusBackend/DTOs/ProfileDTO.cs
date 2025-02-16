namespace ProyectAntivirusBackend.DTOs
{
    public class ProfileDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public required string Preferences { get; set; }
        public required string Biography { get; set; }
        public required string ProfilePicture { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
    }
}