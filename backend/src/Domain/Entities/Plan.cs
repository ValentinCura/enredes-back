using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Plan
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Speed { get; set; } = string.Empty;
        [Required]
        public List<string> Features { get; set; } = new();
        [Required]
        public List<string> Colors { get; set; } = new();
        [Required]
        public bool Featured { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // FK a Localidad
        [Required]
        public int LocalityId { get; set; }
        public Locality Locality { get; set; } = null!;
    }
}