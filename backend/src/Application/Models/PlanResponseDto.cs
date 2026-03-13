namespace Application.Models
{
    public class PlanResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Speed { get; set; } = string.Empty;
        public List<LocalitySimpleDto> Localities { get; set; } = new();
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}