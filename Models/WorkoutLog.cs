using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class WorkoutLog
    {
        
        public int Id { get; set; }

        // Foreign key to workout
        public int WorkoutId { get; set; }

        //[ForeignKey("WorkoutId")]
        public string? Workout { get; set; }

        // Foreign key to user
        public int UserProfileId { get; set; }

        //public UserProfile? UserProfile { get; set; }

        public DateTime Date { get; set; }
        public int ActualDurationMinutes { get; set; }
    }
}
