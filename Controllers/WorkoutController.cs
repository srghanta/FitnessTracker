using AutoMapper;
using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication for all endpoints in this controller
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
            // Ensure there is at least one user
            var user = await _context.UserProfiles.FirstOrDefaultAsync();
            if (user == null)
                return BadRequest("No users exist in the system.");

            var workout = _mapper.Map<Workout>(workoutDto);
            workout.UserProfileId = user.Id;
            workout.UserProfile = user;

            _context.Workouts.Add(workout);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<WorkoutReadDto>(workout);
            return CreatedAtAction(nameof(GetWorkout), new { id = workout.Id }, resultDto);
        }

        // GET: api/workouts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutReadDto>>> GetWorkouts()
        {
            var workouts = await _context.Workouts
                .Include(w => w.UserProfile)
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
