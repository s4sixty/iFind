using AutoMapper;
using FoundItemsService.Database;
using FoundItemsService.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FoundItemsService.Controllers
{
    [Route("api/v{version:apiVersion}/found")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class FoundController : Controller
    {
        readonly DatabaseContext db;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;

        public FoundController(IConfiguration config, IMapper mapper)
        {
            db = new DatabaseContext();
            _config = config;
            _mapper = mapper;
        }

        // GET version/<FoundController>/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var item = db.FoundItems
                .Include(c => c.Comments)
                .Where(c => c.Id == id);
            if (item == null)
                return NotFound();

            FoundItem owner = await db.FoundItems.FindAsync(id);
            int UserId = owner.UserId;
            string token = GenerateJSONWebToken(UserId);

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync("https://ifind-auth.herokuapp.com/api/v1/users/");
            var stringJson = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<object>(stringJson);

            return Ok(new { item, owner = result });
        }

        // GET: version/<FoundController>
        [HttpGet("all")]
        public ActionResult<FoundItem> GetAllAsync()
        {
            var items = db.FoundItems.ToList();

            ICollection<FoundItemDTO> model = _mapper.Map<ICollection<FoundItem>, ICollection<FoundItemDTO>>(items);

            return Ok(model);
        }

        // POST version/<FoundController>
        [HttpPost]
        public IActionResult Post([FromBody] FoundItem item)
        {
            try
            {
                FoundItem itemCheck = db.FoundItems.SingleOrDefault(x => x.Id.Equals(item.Id));
                // check if item exists
                if (itemCheck != null)
                    return Conflict();

                item.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

                // set creation date
                item.CreatedAt = DateTime.UtcNow;
                item.ModifiedAt = DateTime.UtcNow;

                db.FoundItems.Add(item);
                db.SaveChanges();

                return Ok(new { item });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST version/<FoundController>
        [HttpPut]
        public IActionResult Put([FromBody] FoundItem item)
        {
            try
            {
                FoundItem itemCheck = db.FoundItems.SingleOrDefault(x => x.Id.Equals(item.Id));
                // check if username exists
                if (itemCheck == null)
                    return BadRequest();

                // set creation date
                item.ModifiedAt = DateTime.UtcNow;

                db.Update(item);
                db.SaveChanges();

                return Ok(new
                {
                    message = "item modified successefully",
                    item
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // DELETE version/<FoundController>/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            FoundItem item = db.FoundItems.Find(id);
            db.FoundItems.Remove(item);
            db.SaveChanges();
            return Ok(new
            {
                message = "item deleted succesefully.",
                item
            });
        }

        private string GenerateJSONWebToken(int UserId)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(ClaimTypes.NameIdentifier, UserId.ToString())
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