﻿using AutoMapper;
using FoundItemsService.Database;
using FoundItemsService.Database.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundItemsService.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
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

        // GET version/<LostController>/{id}
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var item = db.FoundItems.Find(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // GET: version/<LostController>
        [HttpGet("all")]
        public ActionResult<FoundItem> GetAllAsync()
        {
            var items = db.FoundItems.ToList();

            return Ok(items);
        }

        // POST version/<LostController>
        [HttpPost]
        public IActionResult Post([FromBody] FoundItem item)
        {
            try
            {
                FoundItem itemCheck = db.FoundItems.SingleOrDefault(x => x.Id.Equals(item.Id));
                // check if username exists
                if (itemCheck != null)
                    return Conflict();

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

        // POST version/<LostController>
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

        // DELETE version/<LostController>/{id}
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
    }
}