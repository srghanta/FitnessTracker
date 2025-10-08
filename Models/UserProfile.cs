using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class UserProfile
    {
        public int Id { get; set; }

        // Foreign key to IdentityUser
        public string UserId { get; set; } // Link to IdentityUser

        // Optional: You can add a navigation property to the IdentityUser
        public User User { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        public int Age { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }
}
