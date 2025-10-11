using FitnessTracker.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class UserProfile
{
    public int Id { get; set; }

    [Required]
    [ForeignKey("User")]
    public string UserId { get; set; }  // FK to IdentityUser

    public User User { get; set; }  // navigation property

    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public int Age { get; set; }

    [Required]
    public double Weight { get; set; }

    [Required]
    public double Height { get; set; }
}
