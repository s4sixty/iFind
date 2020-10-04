using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Database;
using UserService.Database.Entities;

namespace UserService.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        DatabaseContext db;
        private IConfiguration _config;
        public RegisterController(IConfiguration config)
        {
            db = new DatabaseContext();
            _config = config;
        }

        // POST version/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            try
            {
                User userCheck = db.Users.SingleOrDefault(x => x.email.Equals(user.email));
                // check if username exists
                if (userCheck != null)
                    return Conflict();

                // Encrypt the password
                user.password = BCrypt.Net.BCrypt.HashPassword(user.password);

                // set creation date
                user.CreatedAt = DateTime.UtcNow;
                user.ModifiedAt = DateTime.UtcNow;

                db.Users.Add(user);
                db.SaveChanges();

                var tokenString = GenerateJSONWebToken(user);
                return Ok(new { user, token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // Function to generate jwt web token
        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.email)
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddDays(3),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
