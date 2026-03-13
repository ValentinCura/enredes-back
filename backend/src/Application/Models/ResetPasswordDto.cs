using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class ResetPasswordDto
    {
        public string Token { get; set; } = string.Empty;
        [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres")]
        public string NewPassword { get; set; } = string.Empty;
    }
}