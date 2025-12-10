using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassHub.Data;
using ClassHub.Models;
using ClassHub.DTOs;
using Microsoft.AspNetCore.Identity;

namespace ClassHub.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly ExternalDbContext _context;
        private readonly PasswordHasher<User> _passwordHash;

        public UserController(ExternalDbContext context)
        {
            _context = context;
            _passwordHash = new PasswordHasher<User>();
        }

        // GET: api/users
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _context.Users
                .Select(u => new
                {
                    u.Id,
                    u.UserName
                })
                .ToList();

            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _context.Users
                .Where(u => u.Id == id)
                .Select(u => new
                {
                    u.Id,
                    u.UserName
                })
                .FirstOrDefault();

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public IActionResult CreateUser([FromBody] CreateUserDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.UserName) ||
                string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Username and password are required");
            }

            if (_context.Users.Any(u => u.UserName == dto.UserName))
            {
                return BadRequest("Username already exists");
            }

            var user = new User
            {
                UserName = dto.UserName,
            };

            user.Password = _passwordHash.HashPassword(user, dto.Password);

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, new
            {
                user.Id,
                user.UserName
            });
        }

        // GET: api/users/{id}/organisations
        [HttpGet("{id}/organisations")]
        public IActionResult GetUserOrganisations(int id)
        {
            var data = _context.UserRoles
                .Where(ur => ur.UserId == id)
                .Include(ur => ur.Organisation)
                .Include(ur => ur.Role)
                .Select(ur => new
                {
                    OrganisationId = ur.Organisation.Id,
                    OrganisationName = ur.Organisation.Name,
                    Role = ur.Role.Name
                })
                .ToList();

            if (!data.Any())
                return NotFound("User has no organisations");

            return Ok(data);
        }
    }
}
