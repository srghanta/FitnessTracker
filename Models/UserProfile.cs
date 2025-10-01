using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Models
{
    public class UserProfile
    {
        //[Key]
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; } = string.Empty;

        public int Age { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        // Navigation property
        //public List<Workout>? Workouts { get; set; }
    }
}
