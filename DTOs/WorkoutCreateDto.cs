using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs
{
    public class WorkoutCreateDto
    {

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        
    }
}