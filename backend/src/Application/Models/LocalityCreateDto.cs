using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class LocalityCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El código es obligatorio")]
        public string Cod { get; set; } = string.Empty;
        [Required(ErrorMessage = "La provincia es obligatoria")]
        public string Province { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
    }
}