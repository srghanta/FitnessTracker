using FitnessTracker.DTOs;
using FitnessTracker.Models;
using FitnessTracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FitnessTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _config;
        private readonly ILogger<AuthController> _logger;
        private readonly ApplicationDbContext _context;

        public AuthController(
            UserManager<User> userManager,
            IConfiguration config,
            ILogger<AuthController> logger,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            _config = config;
            _logger = logger;
            _context = context;
        }

        // ✅ Register endpoint
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new User
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                _logger.LogError("Registration failed for user: {UserName}", model.UserName);
                return BadRequest(result.Errors);
            }

            // Create linked UserProfile
            var profile = new UserProfile
            {
                UserId = user.Id,
                UserName = model.UserName,
                Age = model.Age.Value,
                Height = model.Height.Value,
                Weight = model.Weight.Value
            };


            _context.UserProfiles.Add(profile);
            await _context.SaveChangesAsync();

            _logger.LogInformation("User {UserName} registered successfully", model.UserName);

            return Ok(new
            {
                message = "Registration successful!",
                user = new
                {
                    user.UserName,
                    user.Email,
                    profile.Age,
                    profile.Height,
                    profile.Weight
                }
            });
        }

        // ✅ Login endpoint
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null || !await _userManager.CheckPasswordAsync(user, model.Password))
                return Unauthorized("Invalid username or password.");

            // Add claims for JWT
            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.UtcNow.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
            );

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                username = user.UserName
            });
        }
    }
}
