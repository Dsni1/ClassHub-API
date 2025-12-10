using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassHub.Data;
using ClassHub.Models;
using ClassHub.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ClassHub.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class LoginController : ControllerBase
    {
        private readonly ExternalDbContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        public LoginController(ExternalDbContext context)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<User>();
        }

        // POST: api/auth/login
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
             if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.User_name == request.User_name);


            if (user == null)
                return Unauthorized("Invalid username or password");

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                request.Password
            );

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid username or password");

            // ✅ User authenticated
            return Ok(new LoginResponseDto
            {
                UserId = user.Id,
                Token = "TEMP_TOKEN"
            });
        }
    }
}