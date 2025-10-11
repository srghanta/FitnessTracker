using Microsoft.AspNetCore.Identity;

namespace FitnessTracker.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }

        // Navigation property (1:1 relation with UserProfile)
        public UserProfile? UserProfile { get; set; }
    }
}