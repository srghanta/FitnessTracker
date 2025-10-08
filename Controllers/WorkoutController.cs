using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkoutsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public WorkoutsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/workouts
        [HttpGet]
        public async Task<IActionResult> GetWorkouts()
        {

           

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // This retrieves the UserId from the logged-in user
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (userProfile == null)
                return BadRequest("UserProfile not found");

            var workouts = await _context.Workout
                .Where(w => w.UserProfileId == userProfile.Id)
                .Include(w => w.WorkoutLogs)
                .ToListAsync();

            var userName = User.Identity?.Name;

            return Ok(workouts);
        }

        // POST: api/workouts
        [HttpPost]
        public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.UserId == userId);

            if (userProfile == null)
                return BadRequest("UserProfile not found");

            workout.UserProfileId = userProfile.Id;
            _context.Workout.Add(workout);
            await _context.SaveChangesAsync();

            return Ok(workout);
        }


        // PUT: api/workouts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout workout)
        {
            var userName = User.Identity?.Name;
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (userProfile == null)
                return BadRequest("UserProfile not found");

            var existing = await _context.Workout
                .Where(w => w.Id == id && w.UserProfileId == userProfile.Id)
                .FirstOrDefaultAsync();

            if (existing == null)
                return NotFound();

            existing.Name = workout.Name;
            existing.DurationMinutes = workout.DurationMinutes;

            await _context.SaveChangesAsync();

            return Ok(existing);
        }

        // DELETE: api/workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var userName = User.Identity?.Name;
            var userProfile = await _context.UserProfiles
                .FirstOrDefaultAsync(u => u.UserName == userName);

            if (userProfile == null)
                return BadRequest("UserProfile not found");

            var workout = await _context.Workout
                .Where(w => w.Id == id && w.UserProfileId == userProfile.Id)
                .FirstOrDefaultAsync();

            if (workout == null)
                return NotFound();

            _context.Workout.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
