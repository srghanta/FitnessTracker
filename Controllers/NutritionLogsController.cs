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
    public class NutritionLogController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public NutritionLogController(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        // GET: api/NutritionLog
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var logs = await _context.NutritionLogs
                .Where(l => l.UserId == user.Id) // Filter by logged-in user
                .ToListAsync();

            return Ok(logs);
        }

        // GET: api/NutritionLog/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var log = await _context.NutritionLogs
                .Where(l => l.UserId == user.Id && l.Id == id)
                .FirstOrDefaultAsync();

            if (log == null) return NotFound();
            return Ok(log);
        }

        // POST: api/NutritionLog
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] NutritionLogDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var log = _mapper.Map<NutritionLog>(dto);
            log.UserId = user.Id; // Tie log to logged-in user

            _context.NutritionLogs.Add(log);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = log.Id }, log);
        }

        // PUT: api/NutritionLog/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] NutritionLogDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var log = await _context.NutritionLogs
                .Where(l => l.UserId == user.Id && l.Id == id)
                .FirstOrDefaultAsync();

            if (log == null) return NotFound();

            _mapper.Map(dto, log);
            await _context.SaveChangesAsync();

            return Ok(log);
        }

        // DELETE: api/NutritionLog/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userName = User.Identity?.Name;
            var user = await _userManager.FindByNameAsync(userName);

            var log = await _context.NutritionLogs
                .Where(l => l.UserId == user.Id && l.Id == id)
                .FirstOrDefaultAsync();

            if (log == null) return NotFound();

            _context.NutritionLogs.Remove(log);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
