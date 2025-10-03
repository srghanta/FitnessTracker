using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs
{
    public class NutritionLogDto
    {
        [Required]
        public int UserProfileId { get; set; }

        [Required]
        [StringLength(100)]
        public string MealName { get; set; }

        [Required]
        [Range(0, 5000)]
        public int Calories { get; set; }

        [Range(0, 500)]
        public double Protein { get; set; }

        [Range(0, 500)]
        public double Carbs { get; set; }

        [Range(0, 500)]
        public double Fat { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
