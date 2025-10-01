using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FitnessTracker.Data;
using FitnessTracker.Models;
using FitnessTracker.DTOs;
using AutoMapper;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WorkoutsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/workouts
        [HttpPost]
        public async Task<ActionResult<WorkoutReadDto>> CreateWorkout([FromBody] WorkoutCreateDto workoutDto)
        {
            // Try to find existing user
            var user = await _context.UserProfiles.FindAsync(workoutDto.UserProfileId);

            // If user does not exist, create a new one automatically
            if (user == null)
            {
                user = new UserProfile
                {
                    UserName = $"User{workoutDto.UserProfileId}" // Do NOT set Id manually
                };
                _context.UserProfiles.Add(user);
                await _context.SaveChangesAsync(); // EF will generate the Id
            }

            // Map DTO to Workout entity
            var workout = _mapper.Map<Workout>(workoutDto);

            // Attach the existing (or newly created) user
            workout.UserProfile = user;

            // Save workout
            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            // Map back to DTO
            var resultDto = _mapper.Map<WorkoutReadDto>(workout);

            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, resultDto);
        }

        // GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutReadDto>>> GetWorkouts()
        {
            var workouts = await _context.Workouts
                .Include(w => w.UserProfile) // Include navigation property
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<WorkoutReadDto>>(workouts));
        }

        // GET: api/workouts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutReadDto>> GetWorkout(int id)
        {
            var workout = await _context.Workouts
                .Include(w => w.UserProfile)
                .FirstOrDefaultAsync(w => w.Id == id);

            if (workout == null)
                return NotFound();

            return Ok(_mapper.Map<WorkoutReadDto>(workout));
        }

        // DELETE: api/workouts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkout(int id)
        {
            var workout = await _context.Workouts.FindAsync(id);
            if (workout == null)
                return NotFound();

            _context.Workouts.Remove(workout);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
