using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs
{
    public class WorkoutCreateDto
    {
      
            [Required]
            public string Name { get; set; }

            public int DurationMinutes { get; set; }

            [Required]
            public int UserProfileId { get; set; }  // Just the FK, not the whole UserProfile
        


    }
}