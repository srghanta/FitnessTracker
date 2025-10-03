using AutoMapper;
using FitnessTracker.Data;
using FitnessTracker.DTOs;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class WorkoutLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public WorkoutLogController(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // POST: api/workoutlog
        [HttpPost("{workoutId}")]
        public async Task<ActionResult<WorkoutLogReadDto>> CreateWorkoutLog(int workoutId, [FromBody] WorkoutLogCreateDto logDto)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest("User not found");

            // Ensure the workout belongs to the current user
            var workout = await _context.Workouts
                .FirstOrDefaultAsync(w => w.Id == workoutId && w.UserId == user.Id);

            if (workout == null)
                return BadRequest("Workout not found for this user.");

            var log = _mapper.Map<WorkoutLog>(logDto);
            log.WorkoutId = workout.Id;

            _context.WorkoutLogs.Add(log);
            await _context.SaveChangesAsync();

            var resultDto = _mapper.Map<WorkoutLogReadDto>(log);
            resultDto.WorkoutName = workout.Name; // ensure WorkoutName is populated

            return CreatedAtAction(nameof(GetWorkoutLog), new { id = log.Id }, resultDto);
        }

        // GET: api/workoutlog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutLogReadDto>>> GetWorkoutLogs()
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest("User not found");

            var logs = await _context.WorkoutLogs
                .Include(l => l.Workout)
                .Where(l => l.Workout.UserId == user.Id)
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<WorkoutLogReadDto>>(logs);
            return Ok(result);
        }

        // GET: api/workoutlog/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutLogReadDto>> GetWorkoutLog(int id)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest("User not found");

            var log = await _context.WorkoutLogs
                .Include(l => l.Workout)
                .FirstOrDefaultAsync(l => l.Id == id && l.Workout.UserId == user.Id);

            if (log == null)
                return NotFound();

            var result = _mapper.Map<WorkoutLogReadDto>(log);
            return Ok(result);
        }

        // DELETE: api/workoutlog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutLog(int id)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BadRequest("User not found");

            var log = await _context.WorkoutLogs
                .Include(l => l.Workout)
                .FirstOrDefaultAsync(l => l.Id == id && l.Workout.UserId == user.Id);

            if (log == null)
                return NotFound();

            _context.WorkoutLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
