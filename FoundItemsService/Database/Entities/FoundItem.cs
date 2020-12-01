using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoundItemsService.Database.Entities
{
    public class FoundItem
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public int UserId { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Required]
        public string Name { get; set; }
        public string Model { get; set; }
        [Required]
        public DateTime FoundAt { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Required]
        public string Color { get; set; }
        [StringLength(5000, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 0)]
        public string Description { get; set; }
        public bool OwnerFound { get; set; }
        public DateTime? OwnerFoundAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public string city { get; set; }
        public string Photo { get; set; }
        public ICollection<Comments> Comments { get; set; }
    }

    public class FoundItemDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Model { get; set; }
        [Required]
        public DateTime FoundAt { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 2)]
        [Required]
        public string Color { get; set; }
        [StringLength(5000, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 0)]
        public string Description { get; set; }
        public string city { get; set; }
        public bool FoundOwner { get; set; }
        public DateTime? FoundOwnerAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Photo { get; set; }
    }
}
