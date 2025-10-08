using FitnessTracker.Data;
using FitnessTracker.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requires authentication for all endpoints in this controller
    public class UserProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserProfileController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/UserProfile
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserProfile>>> GetUsers()
        {
            return await _context.UserProfiles.ToListAsync();
        }

        // GET: api/UserProfile/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfile>> GetUser(int id)
        {
            var user = await _context.UserProfiles.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null) return NotFound();
            return user;
        }

        // POST: api/UserProfile
        [HttpPost]

        public async Task<ActionResult<UserProfile>> CreateUser([FromBody] UserProfile userProfile)
        {
            // Ensure User exists in AspNetUsers
            var existingUser = await _context.Users.FindAsync(userProfile.UserId);
            if (existingUser == null)
                return BadRequest("User not found in Identity table.");

            // Don’t set the Id manually — EF will handle it
            userProfile.User = existingUser;

            _context.UserProfiles.Add(userProfile);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = userProfile.Id }, userProfile);
        }

      

        // DELETE: api/UserProfile/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.UserProfiles.FindAsync(id);
            if (user == null) return NotFound();

            _context.UserProfiles.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
