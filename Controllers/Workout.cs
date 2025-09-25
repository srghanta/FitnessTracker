using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public int DurationMinutes { get; set; } // duration in minutes

        [Required]
        public DateTime Date { get; set; }
    }
}
