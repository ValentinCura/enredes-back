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
        public List<string> Features { get; set; } = new();
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<PlanLocality> PlanLocalities { get; set; } = new();
    }
}