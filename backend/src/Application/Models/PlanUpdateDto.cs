namespace Application.Models
{
    public class PlanUpdateDto
    {
        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Speed { get; set; }
        public List<string>? Features { get; set; }
        public bool? Status { get; set; }
        public List<int>? LocalityIds { get; set; }
    }
}