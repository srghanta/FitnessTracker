using System.ComponentModel.DataAnnotations;

public class RegisterDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    // Optional profile fields
    public int Age { get; set; } = 0;
    public double Weight { get; set; } = 0;
    public double Height { get; set; } = 0;
}
