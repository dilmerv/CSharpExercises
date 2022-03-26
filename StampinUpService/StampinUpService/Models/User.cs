using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using StampinUp.Service.Services;

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


        /* gorest.co.in --
           This API service doesn't have an endpoint to get by email, only has one for id.
           Because the User.Id of our endpoint is a guid (and in real world instances,
            we most likely wouldn't have the id of this thirdparty service, so we're choosing
            to use the email as the key. So, we get all users and then take the firstordefault
            that has that email. This isn't the ideal, but we're just practicing/playing so...
        */

        private readonly IGoRESTApiService _goRESTApiService;

        public User(Guid id, string email, string name, string country, string catchphrase, List<UserPlatform> userplatforms, IGoRESTApiService goRESTApiService)
        {
            Id = id;
            Email = email;
            Name = name;
            Country = country;
            CatchPhrase = catchphrase;
            UserPlatforms = userplatforms;
            //Consume the third party API and use the gender and status from that for our users
            _goRESTApiService = goRESTApiService;
            List<GoRESTApiUserInfo> goRESTUsers = GoRESTUsers().Result;
            GoRESTApiUserInfo goRESTApiUserInfo = goRESTUsers.FirstOrDefault(u => u.email == email);
            if (goRESTApiUserInfo != null)
            {
                GoRESTGender = goRESTApiUserInfo.gender;
                GoRESTStatus = goRESTApiUserInfo.status;
            }
        }

        private async Task<List<GoRESTApiUserInfo>> GoRESTUsers()
        {
            return await _goRESTApiService.GetUsers();
        }
    }
}
