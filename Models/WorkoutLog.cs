namespace FitnessTracker.Models
{
    public class WorkoutLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = string.Empty; // e.g. Running, Yoga
        public int Duration { get; set; } // in minutes
        public int CaloriesBurned { get; set; }
    }
}
