public class WorkoutLogReadDto
{
    public int Id { get; set; }
    public string Notes { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string WorkoutName { get; set; } // Optionally add the Workout's name for display
}
