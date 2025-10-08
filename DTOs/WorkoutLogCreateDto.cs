public class WorkoutLogCreateDto
{
    public string Notes { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.UtcNow;
    public int WorkoutId { get; set; }  // Instead of sending the full Workout object, send just the ID
}
