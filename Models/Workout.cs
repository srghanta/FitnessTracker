using FitnessTracker.Models;
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

        public User User { get; set; }      // navigation

        // Identity UserId
        [Required]
        public string UserId { get; set; } = string.Empty;

        // Optional: store UserName for mapping
        public string UserName { get; set; } = string.Empty;

        public ICollection<WorkoutLog> Logs { get; set; } = new List<WorkoutLog>();
    }
}