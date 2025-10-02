using FitnessTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WeightLog> WeightLogs { get; set; }
        public DbSet<Workout> Workouts { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WorkoutLog> WorkoutLogs { get; set; }
        public DbSet<NutritionLog> NutritionLogs { get; set; }
        public DbSet<Goal> Goals { get; set; }
    }
}
