using System.ComponentModel.DataAnnotations;

public class CreateInstitutionDTO
{
    [Required(ErrorMessage = "The Name field is required.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "The Image field is required.")]
    public string Image { get; set; } = string.Empty;
}
