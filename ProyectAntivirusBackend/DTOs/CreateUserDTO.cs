using System.ComponentModel.DataAnnotations;
namespace ProyectAntivirusBackend.DTOs


{
    public class CreateUserDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string Role { get; set; } = string.Empty;

        [Required]
        [RegularExpression("abierta|cerrada|próxima", ErrorMessage = "El estado debe ser 'abierta', 'cerrada' o 'próxima'.")]
        public string Status { get; set; } = "abierta"; // Estado por defecto: "abierta"
    }
}