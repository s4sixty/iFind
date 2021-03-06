﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace UserService.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Required]
        public string firstName { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        [Required]
        public string lastName { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        [MinLength(6)]
        public string password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [JsonIgnore]
        public string Role { get; set; }
    }

    public class UserDTO
    {
        public int Id { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string firstName { get; set; }
        [StringLength(20, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 3)]
        public string lastName { get; set; }
        [EmailAddress]
        public string email { get; set; }
    }

    public class AuthenticationModel
    {
        [EmailAddress]
        public string email { get; set; }
        [MinLength(6)]
        public string password { get; set; }
    }
}
