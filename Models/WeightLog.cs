namespace FitnessTracker.Models
{
    public class WeightLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public decimal Weight { get; set; }
    }
}
