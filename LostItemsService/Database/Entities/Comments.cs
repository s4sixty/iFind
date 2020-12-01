using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LostItemsService.Database.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public LostItem LostItem { get; set; }
    }
}
