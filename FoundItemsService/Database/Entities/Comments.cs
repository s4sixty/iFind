using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoundItemsService.Database.Entities
{
    public class Comments
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public FoundItem FoundItem { get; set; }
    }
}
