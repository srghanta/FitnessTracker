using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Models;

namespace FitnessTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<WeightLog> WeightLogs { get; set; }
        public DbSet<Workout> Workout { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<WorkoutLog> WorkoutLog { get; set; }
        public DbSet<NutritionLog> NutritionLogs { get; set; }
        public DbSet<Goal> Goals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Ensures Identity configuration is applied

            // Configure relationships
            modelBuilder.Entity<Workout>()
                .HasMany(w => w.WorkoutLogs)
                .WithOne(l => l.Workout)
                .HasForeignKey(l => l.WorkoutId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserProfile>()
                .HasOne(up => up.User)
                .WithOne()
                .HasForeignKey<UserProfile>(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
