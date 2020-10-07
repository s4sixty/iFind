using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class Role
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
}
