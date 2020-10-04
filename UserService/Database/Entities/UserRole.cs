using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class UserRole
    {
        public int IdUser { get; set; }
        public int IdRole { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
