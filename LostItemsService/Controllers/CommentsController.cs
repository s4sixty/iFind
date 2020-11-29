using AutoMapper;
using LostItemsService.Database;
using LostItemsService.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LostItemsService.Controllers
{
    [Route("api/v{version:apiVersion}/lost")]
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

        // GET version/<LostController>/{id}
        [HttpPost("{id}/comments")]
        public IActionResult Post([FromBody] Comments comment, int id)
        {
            var item = db.LostItems.Find(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }
    }
}
