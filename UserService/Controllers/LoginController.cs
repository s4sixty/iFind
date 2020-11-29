using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UserService.Database;
using UserService.Database.Entities;

namespace UserService.Controllers
{
    [Route("api/v{version:apiVersion}/login")]
    [ApiVersion("1.0")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly DatabaseContext db;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public LoginController(IConfiguration config, IMapper mapper)
        {
            db = new DatabaseContext();
            _config = config;
            _mapper = mapper;
        }

        // POST version/<UserController>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] AuthenticationModel data)
        {
            try
            {
                User user = await db.Users.SingleOrDefaultAsync(x => x.email.Equals(data.email));
                // check if email exists
                if (user == null)
                    return Unauthorized(new { message = "Invalid email", user, data.email });

                // check if password is correct
                if (!BCrypt.Net.BCrypt.Verify(data.password, user.password))
                    return Unauthorized("Invalid password");

                var tokenString = GenerateJSONWebToken(user);

                var model = _mapper.Map<UserDTO>(user);

                return Ok(new 
                { 
                    user = model,
                    token = tokenString
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        private string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, userInfo.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userInfo.email),
                new Claim(ClaimTypes.Role, userInfo.Role)
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
