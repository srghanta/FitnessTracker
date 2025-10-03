using System;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.DTOs
{
    public class GoalDto

    {
        
            public string GoalType { get; set; } = string.Empty;
            public decimal CurrentValue { get; set; }
            public DateTime Deadline { get; set; }
        

        [Required]
        public int UserProfileId { get; set; }

        [Required]
        [StringLength(100)]
        public string GoalName { get; set; }

        [Required]
        [Range(0, 1000)]
        public double TargetValue { get; set; }

        [Required]
        public DateTime TargetDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
