using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LostItemsService.Database.Entities
{
    public class LostItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int UserId { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Required]
        public string name { get; set; }
        [Required]
        public DateTime LostAt { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Required]
        public string color { get; set; }
        [StringLength(5000, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 0)]
        public string description { get; set; }
        [DataType(DataType.PhoneNumber)]
        public string MobilePhone { get; set; }
        public bool found { get; set; }
        public DateTime? foundAt { get; set; }
    }
}
