namespace FitnessTracker.Models
{
    public class NutritionLog
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty; // optional for DTO
        public DateTime Date { get; set; }
        public int CaloriesConsumed { get; set; }
        public int Protein { get; set; }
        public int Carbs { get; set; }
        public int Fat { get; set; }
    }
}
