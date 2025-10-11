using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs
{
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

        [Required]
        [Range(1, 150)]
        public int? Age { get; set; }

        [Required]
        [Range(30, 300)]
        public double? Weight { get; set; }

        [Required]
        [Range(50, 250)]
        public double? Height { get; set; }
    }
}
