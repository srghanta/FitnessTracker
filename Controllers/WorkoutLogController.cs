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
    [Authorize]
    public class WorkoutLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public WorkoutLogController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // POST: api/workoutlog/{workoutId}
        [HttpPost("{workoutId}")]
        public async Task<ActionResult<WorkoutLogReadDto>> CreateWorkoutLog(int workoutId, [FromBody] WorkoutLogCreateDto logDto)
        {
            // Find the workout
            var workout = await _context.Workout
                .Include(w => w.WorkoutLogs)
                .FirstOrDefaultAsync(w => w.Id == workoutId);

            if (workout == null)
                return BadRequest("Workout not found");

            // Map DTO to entity
            var log = _mapper.Map<WorkoutLog>(logDto);
            log.WorkoutId = workout.Id;  // Ensure the correct WorkoutId is set

            _context.WorkoutLog.Add(log);
            await _context.SaveChangesAsync();

            // Map entity to DTO for response
            var resultDto = _mapper.Map<WorkoutLogReadDto>(log);
            resultDto.WorkoutName = workout.Name;  // Optionally include WorkoutName in response DTO

            return CreatedAtAction(nameof(GetWorkoutLog), new { id = log.Id }, resultDto);
        }

        // GET: api/workoutlog
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkoutLogReadDto>>> GetWorkoutLogs()
        {
            var logs = await _context.WorkoutLog
                .Include(l => l.Workout)  // Include Workout info for each log
                .ToListAsync();

            var result = _mapper.Map<IEnumerable<WorkoutLogReadDto>>(logs);
            return Ok(result);
        }

        // GET: api/workoutlog/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<WorkoutLogReadDto>> GetWorkoutLog(int id)
        {
            var log = await _context.WorkoutLog
                .Include(l => l.Workout)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (log == null)
                return NotFound();

            var result = _mapper.Map<WorkoutLogReadDto>(log);
            result.WorkoutName = log.Workout.Name;  // Include WorkoutName for display

            return Ok(result);
        }

        // DELETE: api/workoutlog/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWorkoutLog(int id)
        {
            var log = await _context.WorkoutLog
                .FirstOrDefaultAsync(l => l.Id == id);

            if (log == null)
                return NotFound();

            _context.WorkoutLog.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
