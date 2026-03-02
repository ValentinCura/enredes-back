namespace Application.Models
{
    public class LocalityResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Cod { get; set; } = string.Empty;
        public string Province { get; set; } = string.Empty;
        public bool Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}