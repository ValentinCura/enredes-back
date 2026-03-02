namespace Application.Models
{
    public class PlanResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Speed { get; set; } = string.Empty;
        public List<string> Features { get; set; } = new();
        public List<string> Colors { get; set; } = new();
        public bool Featured { get; set; }
        public int LocalityId { get; set; }
        public string LocalityName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}