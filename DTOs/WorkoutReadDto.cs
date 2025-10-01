namespace FitnessTracker.DTOs
{
    public class WorkoutReadDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int DurationMinutes { get; set; }

        public DateTime Date { get; set; }

        public int UserProfileId { get; set; }

        public string? UserName { get; set; }  // Optional: populate from UserProfile
    }
}
