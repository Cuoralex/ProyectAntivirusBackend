using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectAntivirusBackend.Models
{
    [Table("opportunities")] // Especifica el nombre de la tabla en la base de datos
    public class Opportunity
    {
        [Key] // Indica que esta propiedad es la clave primaria
        [Column("id")]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required] // Indica que este campo es obligatorio
        [Column("title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Column("description")]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Column("type")]
        public string Type { get; set; } = string.Empty; // Usaremos string en lugar de ENUM

        [Required]
        [Column("sector")]
        public string Sector { get; set; } = string.Empty; // Usaremos string en lugar de ENUM

        [Required]
        [Column("ubication")]
        public string Location { get; set; } = string.Empty;

        [Required]
        [Column("requirements")]
        public string Requirements { get; set; } = string.Empty;

        [Required]
        [Column("benefits")]
        public string Benefits { get; set; } = string.Empty;

        [Column("publication_date")]
        public DateTime PublicationDate { get; set; } = DateTime.UtcNow;

        [Required]
        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Required]
        [Column("institution")]
        public string Institution { get; set; } = string.Empty;

        [Column("state")]
        public string Status { get; set; } = "open"; // Estado por defecto: "abierta"

        // Método para validar el estado
        public bool IsValidStatus()
        {
            return Status == "open" || Status == "close" || Status == "next";
        }
    }
}