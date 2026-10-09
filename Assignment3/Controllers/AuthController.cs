
using Assignment3.Data;
using Assignment3.DTOS.Usre;
using Assignment3.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment3.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IConfiguration configuration;

        public AuthController(
            AppDbContext context,
            IConfiguration configuration)
        {
            this.context = context;
            this.configuration = configuration;
        }
        [HttpPost("Login")]
        public IActionResult Login(LoginDto dto)
        {
            var user = context.Users
                .FirstOrDefault(u => u.UserName == dto.UserName);

            if (user == null)
            {
                return Unauthorized("Invalid username or password");
            }

            bool isValid = BCrypt.Net.BCrypt.Verify(
                dto.Passwrd,
                user.PasswrdHash
            );

            if (!isValid)
            {
                return Unauthorized("Invalid username or password");
            }

            var claims = GetClaims(user);

            var token = GenerationToken(claims);

            return Ok(new
            {
                Token = token,
                UserName = user.UserName,
                Role = user.Role
            });
        }
        [HttpPost("Register")]
        public IActionResult Register(RegisterDto dto)
        {
            bool existingUser = context.Users
                .Any(u => u.UserName == dto.UserName);

            if (existingUser)
            {
                return BadRequest("Username already exists");
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(
                dto.PasswrdHash
            );

            var user = new User
            {
                UserName = dto.UserName,
                PasswrdHash = passwordHash,
                Role = "User"
            };

            context.Users.Add(user);
            context.SaveChanges();

            return Ok("User registered successfully");
        }
        private List<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role)
            };

            return claims;
        }
        private string GenerationToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    configuration["Jwt:Key"]!
                )
            );

            var creds = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var duration = double.Parse(
                configuration["Jwt:DurationInMinutes"]!
            );

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(duration),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler()
                .WriteToken(token);
        }
    }
}
