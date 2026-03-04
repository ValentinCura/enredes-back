using System.ComponentModel.DataAnnotations;

public class LocalityUpdateDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public string Cod { get; set; } = string.Empty;
    [Required]
    public string Province { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
}