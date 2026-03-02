using System.ComponentModel.DataAnnotations;

namespace Application.Models
{
    public class PlanCreateDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "El precio es obligatorio")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "La velocidad es obligatoria")]
        public string Speed { get; set; } = string.Empty;
        [Required(ErrorMessage = "Las features son obligatorias")]
        public List<string> Features { get; set; } = new();
        [Required(ErrorMessage = "Los colores son obligatorios")]
        public List<string> Colors { get; set; } = new();
        public bool Featured { get; set; } = false;
        [Required(ErrorMessage = "La localidad es obligatoria")]
        public int LocalityId { get; set; }
    }
}