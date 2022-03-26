using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StampinUp.Service.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Must be valid email format")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Country { get; set; }

        public string CatchPhrase { get; set; }

        public List<UserPlatform> UserPlatforms { get; set; }

        public User(Guid id, string email, string name, string country, string catchphrase, List<UserPlatform> userplatforms)
        {
            Id = id;
            Email = email;
            Name = name;
            Country = country;
            CatchPhrase = catchphrase;
            UserPlatforms = userplatforms;            
        }
    }
}
