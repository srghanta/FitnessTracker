using Microsoft.AspNetCore.Identity;

namespace FitnessTracker.Models
{
    public class User : IdentityUser
    {
        // Extra profile info if needed
        public string? FullName { get; set; }
    }
}
