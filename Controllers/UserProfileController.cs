using AutoMapper;
using FitnessTracker.Data;
using FitnessTracker.DTOs;
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
    public class UserProfileController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public UserProfileController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // ✅ GET: api/UserProfile/me
        [HttpGet("me")]
        public async Task<ActionResult<UserProfileDto>> GetMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Invalid user session.");

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found.");

            return Ok(_mapper.Map<UserProfileDto>(profile));
        }

        // ✅ PUT: api/UserProfile/me
        [HttpPut("me")]
        public async Task<IActionResult> UpdateMyProfile([FromBody] UserProfileDto dto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Invalid user session.");

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found.");

            // Map changes (Age, Height, Weight, etc.)
            _mapper.Map(dto, profile);
            await _context.SaveChangesAsync();

            return Ok(_mapper.Map<UserProfileDto>(profile));
        }

        // ✅ DELETE (optional): api/UserProfile/me
        [HttpDelete("me")]
        public async Task<IActionResult> DeleteMyProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized("Invalid user session.");

            var profile = await _context.UserProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
            if (profile == null)
                return NotFound("Profile not found.");

            _context.UserProfiles.Remove(profile);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
