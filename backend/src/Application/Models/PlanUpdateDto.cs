using System.ComponentModel.DataAnnotations;

public class PlanUpdateDto
{
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
    public bool Featured { get; set; } = false;
    public bool Status { get; set; } = true;
    [Required]
    public int LocalityId { get; set; }
}