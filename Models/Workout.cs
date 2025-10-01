using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(500)]
        public string Name { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign key
        [Required]
        public int UserProfileId { get; set; }

        // Navigation property
        public UserProfile UserProfile { get; set; } = null!;

        // Navigation property to logs
        public ICollection<WorkoutLog> Logs { get; set; } = new List<WorkoutLog>();
    }
}
