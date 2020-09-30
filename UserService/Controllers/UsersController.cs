using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        public UsersController()
        {
            db = new DatabaseContext();
        }

        // GET: version/<UserController>
        [HttpGet]
        public async Task<ActionResult<User>> GetAsync()
        {
            int idClaims = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            User user = await db.Users.FindAsync(idClaims);

            return user;
        }

        // GET version/<UserController>/{id}
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return db.Users.Find(id);
        }

        // POST version/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User model)
        {
            try
            {
                db.Users.Add(model);
                db.SaveChanges();
                return StatusCode(201, model);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }

        // PUT version/<UserController>/{id}
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
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
