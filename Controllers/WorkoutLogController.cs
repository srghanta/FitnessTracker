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
    public class WorkoutLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutLogController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/workoutlog
        [HttpPost]
        public async Task<ActionResult<WorkoutLogReadDto>> CreateWorkoutLog([FromBody] WorkoutLogCreateDto logDto)
        {
            // Ensure there is at least one workout
            var workout = await _context.Workouts.FirstOrDefaultAsync();
            if (workout == null)
                return BadRequest("No workouts exist in the system.");

            var log = _mapper.Map<WorkoutLog>(logDto);
            log.WorkoutId = workout.Id;
            log.Workout = workout;

            _context.WorkoutLogs.Add(log);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<WorkoutLogReadDto>(log);
            return CreatedAtAction(nameof(GetWorkoutLog), new { id = log.Id }, resultDto);
        }

        // GET: api/workoutlog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutLogReadDto>>> GetWorkoutLogs()
        {
            var logs = await _context.WorkoutLogs
                .Include(l => l.Workout)
                .ThenInclude(w => w.UserProfile)
                .ToListAsync();

            return Ok(_mapper.Map<IEnumerable<WorkoutLogReadDto>>(logs));
        }

        // GET: api/workoutlog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutLogReadDto>> GetWorkoutLog(int id)
        {
            var log = await _context.WorkoutLogs
                .Include(l => l.Workout)
                .ThenInclude(w => w.UserProfile)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (log == null)
                return NotFound();

            return Ok(_mapper.Map<WorkoutLogReadDto>(log));
        }

        // DELETE: api/workoutlog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutLog(int id)
        {
            var log = await _context.WorkoutLogs.FindAsync(id);
            if (log == null)
                return NotFound();

            _context.WorkoutLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
