namespace FitnessTracker.DTOs
{
    public class WorkoutLogCreateDto
    {
        public string Notes { get; set; } = string.Empty;
        public int WorkoutId { get; set; }
        public DateTime? Date { get; set; }
    }
}
