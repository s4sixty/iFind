using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class Role
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 1)]
        public string Name { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
