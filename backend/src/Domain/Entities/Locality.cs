using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Locality
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Cod { get; set; } = string.Empty;
        [Required]
        public string Province { get; set; } = string.Empty;
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // Navegación
        public ICollection<Plan> Plans { get; set; } = new List<Plan>();
    }
}