using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProductApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost("login")]
        public IActionResult Login(UserLogin login)
        {
            string role = null;

            if (login.Username == "admin" && login.Password == "admin123")
                role = "Admin";
            else if (login.Username == "manager" && login.Password == "manager123")
                role = "Manager";
            else if (login.Username == "viewer" && login.Password == "viewer123")
                role = "Viewer";

            if (role == null)
                return Unauthorized("Invalid credentials");

            var token = GenerateToken(role);

            return Ok(new { token });
        }

        private string GenerateToken(string role)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Name, role)
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // Model for login
    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}