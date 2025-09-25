namespace FitnessTracker.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string GoalType { get; set; } = string.Empty; // Weight/Calories/Workout
        public decimal TargetValue { get; set; }
        public decimal CurrentValue { get; set; }
        public DateTime Deadline { get; set; }
    }
}
