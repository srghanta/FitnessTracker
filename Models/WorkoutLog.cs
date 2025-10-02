using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class WorkoutLog
    {
        public int Id { get; set; }

        [Required]
        public string Notes { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign key to Workout
        public int WorkoutId { get; set; }

        // Navigation property to Workout
        public Workout? Workout { get; set; }
    }
}
