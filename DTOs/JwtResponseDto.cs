namespace FitnessTracker.DTOs
{
    public class JwtResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expiration { get; set; }
    }
}
