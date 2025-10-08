using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class WorkoutLog
    {
        public int Id { get; set; }

        
        public string Notes { get; set; } = string.Empty;

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign key to Workout
        
        public int WorkoutId { get; set; }

        // Navigation property
        public Workout Workout { get; set; } 

     

    }
}
