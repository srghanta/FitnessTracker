using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class WorkoutsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<User> _userManager;

    public WorkoutsController(ApplicationDbContext context, UserManager<User> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetWorkouts()
    {
        var userName = User.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return BadRequest("User not found");

        var workouts = await _context.Workouts
            .Where(w => w.UserId == user.Id)
            .ToListAsync();

        // Populate UserName
        workouts.ForEach(w => w.UserName = user.UserName);

        return Ok(workouts);
    }

    [HttpPost]
    public async Task<IActionResult> CreateWorkout([FromBody] Workout workout)
    {
        var userName = User.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return BadRequest("User not found");

        workout.UserId = user.Id;
        workout.UserName = user.UserName;
        workout.Date = DateTime.UtcNow;

        _context.Workouts.Add(workout);
        await _context.SaveChangesAsync();

        return Ok(workout);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateWorkout(int id, [FromBody] Workout workout)
    {
        var userName = User.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return BadRequest("User not found");

        var existing = await _context.Workouts
            .Where(w => w.Id == id && w.UserId == user.Id)
            .FirstOrDefaultAsync();

        if (existing == null) return NotFound();

        existing.Name = workout.Name;
        existing.DurationMinutes = workout.DurationMinutes;
        await _context.SaveChangesAsync();

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteWorkout(int id)
    {
        var userName = User.Identity?.Name;
        var user = await _userManager.FindByNameAsync(userName);
        if (user == null) return BadRequest("User not found");

        var workout = await _context.Workouts
            .Where(w => w.Id == id && w.UserId == user.Id)
            .FirstOrDefaultAsync();

        if (workout == null) return NotFound();

        _context.Workouts.Remove(workout);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
