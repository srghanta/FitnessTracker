using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FitnessTracker.Models
{
    public class Workout
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;

        // Foreign key to user
        public int UserProfileId { get; set; }

        //[ForeignKey("UserProfileId")]
        public UserProfile? UserProfile { get; set; }

        // Navigation property to logs
        public ICollection<WorkoutLog> Logs { get; set; } = new List<WorkoutLog>();
    }
}
