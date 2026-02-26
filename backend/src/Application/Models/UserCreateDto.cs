using System.ComponentModel.DataAnnotations;

namespace Application.Models;

public class UserCreateDto
{
    [Required(ErrorMessage = "El email es obligatorio")]
    public string Email { get; set; } = string.Empty;
    [Required(ErrorMessage = "La contraseña es obligatoria")]
    public string Password { get; set; } = string.Empty;
    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string FirstName { get; set; } = string.Empty;
    [Required(ErrorMessage = "El apellido es obligatorio")]
    public string LastName { get; set; } = string.Empty;
    [Required(ErrorMessage = "El numero de cliente es obligatorio")]
    public string ClientNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "El numero de telefono es obligatorio")]
    public string Phonenumber { get; set; } = string.Empty;
}