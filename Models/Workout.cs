using FitnessTracker.Models;
using System.ComponentModel.DataAnnotations;

public class Workout
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;
    public int DurationMinutes { get; set; }

    // Link to UserProfile
    [Required]
    public int UserProfileId { get; set; }
    public UserProfile UserProfile { get; set; }

    public ICollection<WorkoutLog> WorkoutLogs { get; set; }
}
