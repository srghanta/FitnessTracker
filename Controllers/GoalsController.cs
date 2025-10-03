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
    [Authorize] // Only authenticated users
    public class GoalController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public GoalController(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/Goal
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var goals = await _context.Goals
                .Where(g => g.UserId == user.Id) // Filter by logged-in user
                .ToListAsync();

            return Ok(goals);
        }

        // GET: api/Goal/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var goal = await _context.Goals
                .Where(g => g.UserId == user.Id && g.Id == id)
                .FirstOrDefaultAsync();

            if (goal == null) return NotFound();
            return Ok(goal);
        }

        // POST: api/Goal
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GoalDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var goal = _mapper.Map<Goal>(dto);
            goal.UserId = user.Id; // Tie goal to logged-in user

            _context.Goals.Add(goal);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = goal.Id }, goal);
        }

        // PUT: api/Goal/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GoalDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var goal = await _context.Goals
                .Where(g => g.UserId == user.Id && g.Id == id)
                .FirstOrDefaultAsync();

            if (goal == null) return NotFound();

            _mapper.Map(dto, goal);
            await _context.SaveChangesAsync();

            return Ok(goal);
        }

        // DELETE: api/Goal/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var goal = await _context.Goals
                .Where(g => g.UserId == user.Id && g.Id == id)
                .FirstOrDefaultAsync();

            if (goal == null) return NotFound();

            _context.Goals.Remove(goal);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
