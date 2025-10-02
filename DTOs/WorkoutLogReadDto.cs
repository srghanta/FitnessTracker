namespace FitnessTracker.DTOs
{
    public class WorkoutLogReadDto
    {
        public int Id { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime Date { get; set; }

        // Nested Workout info if needed
        public int WorkoutId { get; set; }
        public string? WorkoutName { get; set; }
    }
}
