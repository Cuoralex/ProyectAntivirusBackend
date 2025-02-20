namespace ProyectAntivirusBackend.DTOs
{
    public class CreateUserDTO
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Role { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
