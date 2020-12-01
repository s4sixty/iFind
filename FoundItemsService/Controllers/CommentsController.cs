using AutoMapper;
using FoundItemsService.Database;
using FoundItemsService.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FoundItemsService.Controllers
{
    [Route("api/v{version:apiVersion}/Found")]
    [ApiVersion("1.0")]
    [ApiController]
    [Authorize]
    public class CommentsController : Controller
    {
        readonly DatabaseContext db;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        public CommentsController(IConfiguration config, IMapper mapper)
        {
            db = new DatabaseContext();
            _config = config;
            _mapper = mapper;
        }

        // GET version/<FoundController>/{id}
        [HttpPost("{id}/comments")]
        public async Task<IActionResult> PostAsync([FromBody] Comments comment, int id)
        {

            FoundItem item = await db.FoundItems.FindAsync(id);
            if (item == null)
                return NotFound();
            comment.UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            // set creation date
            comment.CreatedAt = DateTime.UtcNow;
            comment.FoundItem = item;
            db.Comments.Add(comment);
            //item.Comments.Add(comment);
            db.SaveChanges();

            return Ok(new { comment, item });
        }

        // GET version/<FoundController>/{id}
        [HttpDelete("{id}/comments/{commentId}")]
        public IActionResult Delete(int id, int commentId)
        {

            var comment = db.Comments.Find(commentId);
            if (comment == null)
                return NotFound();
            int UserId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            if (comment.UserId == UserId)
            {
                db.Comments.Remove(comment);
                db.SaveChanges();
                return Ok(new
                {
                    message = "comment deleted succesefully.",
                    comment
                });
            }
            else
            {
                return Unauthorized(new
                {
                    message = "Not authorized"
                });
            }

        }
    }
}
