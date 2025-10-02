using Microsoft.AspNetCore.Identity;

namespace FitnessTracker.Models
{
    public class User : IdentityUser
    {
        // Add any extra properties you need
        public string FullName { get; set; }
    }
}
