namespace FitnessTracker.DTOs
{
    public class RegisterDto
    {
        public string UserName { get; set; } = null!; // Required
        public string Password { get; set; } = null!; // Required
        public string? Email { get; set; } // Optional
    }
}
