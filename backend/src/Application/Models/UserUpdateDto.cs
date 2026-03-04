using System.ComponentModel.DataAnnotations;

public class UserUpdateDto
{
    [Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    public string LastName { get; set; } = string.Empty;
    [Required]
    public string ClientNumber { get; set; } = string.Empty;
    [Required]
    public string Phonenumber { get; set; } = string.Empty;
    public bool Status { get; set; } = true;
}