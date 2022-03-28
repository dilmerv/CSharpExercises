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

        public string GoRESTGender { get; set; }

        public string GoRESTStatus { get; set; }

    }
}
