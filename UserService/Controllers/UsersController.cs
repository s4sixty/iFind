using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserService.Database;
using UserService.Database.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        DatabaseContext db;
        private IMapper _mapper;
        public UsersController(IMapper mapper)
        {
            db = new DatabaseContext();
            _mapper = mapper;
        }

        // GET: version/<UserController>
        [Authorize(Roles = Role.Admin)]
        [HttpGet("all")]
        public ActionResult<User> GetAllAsync()
        {
            var users = db.Users.ToList();

            return Ok(users);
        }

        // GET: version/<UserController>
        [HttpGet]
        public async Task<ActionResult<User>> GetAsync()
        {
            int idClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = await db.Users.FindAsync(idClaims);

            var model = _mapper.Map<UserDTO>(user);

            return Ok(model);
        }

        // GET version/<UserController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user =  db.Users.Find(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // PUT version/<UserController>/{id}
        [HttpPut("{id}")]
        public void PutId(int id, [FromBody] string value)
        {

        }

        // PUT version/<UserController>
        [HttpPut()]
        public async Task<ActionResult> PutAsync([FromBody] User model)
        {
            int idClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = await db.Users.FindAsync(idClaims);

            if (user == null)
                return BadRequest();

            user.firstName = model.firstName;
            user.firstName = model.firstName;
            // Encrypt the password
            user.password = BCrypt.Net.BCrypt.HashPassword(model.password);

            // set modification date
            user.ModifiedAt = DateTime.UtcNow;

            db.Update(user);
            db.SaveChanges();

            return Ok(new {
                message = "user modified successefully",
                user
            });
        }

        // DELETE version/<UserController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
            db.SaveChanges();
            return Ok(new {
                message = "user deleted succesefully.",
                user
            });
        }
    }
}
