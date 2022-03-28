using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StampinUp.Service.Models;
using StampinUp.Service.Services;

namespace StampinUp.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IGoRESTApiService goRestApiService;

        private List<User> users = new List<User>();

        public UsersController(IGoRESTApiService goRestService)
        {
            /* gorest.co.in --
               This API service doesn't have an endpoint to get by email, only has one for id.
               Because the User.Id of our endpoint is a guid (and in real world instances,
                we most likely wouldn't have the id of this thirdparty service, so we're choosing
                to use the email as the key. So, we get all users and then take the firstordefault
                that has that email. This isn't the ideal, but we're just practicing/playing so...
            */

            //Consume the third party API and use the gender and status from that for our users
            goRestApiService = goRestService;
            List<GoRESTApiUserInfo> goRESTUsers = GoRESTUsers().Result;

            User myUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = "supergrover@sesamestreet.com",
                Name = "Super Grover",
                Country = "Crashland",
                CatchPhrase = "Up Up and Away!",
                UserPlatforms = new List<UserPlatform>()
                        {
                            new UserPlatform()
                            {
                                DeviceId = 1,
                                DeviceName = "VR Platform",
                                DevicePurchaseDate = new DateTime(2022, 1, 1, 0, 0, 0)
                            },
                            new UserPlatform()
                            {
                                DeviceId = 2,
                                DeviceName = "PS5 Platform",
                                DevicePurchaseDate = new DateTime(2020, 11, 12, 0, 0, 0)
                            }
                        }
            };
            AddGoRESTUserInfo(goRESTUsers, myUser);
            users.Add(myUser);

            myUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = "oscarthegrouch@sesamestreet.com",
                Name = "Oscar The Grouch",
                Country = "Garbagecanbul",
                CatchPhrase = "Scram! Get Lost!",
                UserPlatforms = new List<UserPlatform>()
            };
            AddGoRESTUserInfo(goRESTUsers, myUser);
            users.Add(myUser);

            myUser = new User()
            {
                Id = new Guid("0ba44840-524e-4013-beb0-6340461dcba3"), //Hard code guid for testing GetById,Put,Patch
                Email = "cookiemonster@sesamestreet.com",
                Name = "Cookie Monster",
                Country = "Isle of Cookie",
                CatchPhrase = "num num num num",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "Xbox Series S Platform",
                        DevicePurchaseDate = new DateTime(2020, 11, 10, 0, 0, 0)
                    }
                }
            };
            AddGoRESTUserInfo(goRESTUsers, myUser);
            users.Add(myUser);

            myUser = new User()
            {
                Id = Guid.NewGuid(),
                Email = "varma_manik@feil-macgyver.name",
                Name = "Go Rest API User",
                Country = "United States",
                CatchPhrase = "Go Get Me Some 3rd Party restful data",
                UserPlatforms = new List<UserPlatform>()
                {
                    new UserPlatform()
                    {
                        DeviceId = 1,
                        DeviceName = "VR Platform",
                        DevicePurchaseDate =  new DateTime(2022, 1, 1, 0, 0, 0)
                    },
                    new UserPlatform()
                    {
                        DeviceId = 2,
                        DeviceName = "PS5 Platform",
                        DevicePurchaseDate = new DateTime(2020, 11, 12, 0, 0, 0)
                    }
                }
            };
            AddGoRESTUserInfo(goRESTUsers, myUser);
            users.Add(myUser);
        }

        private static void AddGoRESTUserInfo(List<GoRESTApiUserInfo> goRESTUsers, User myUser)
        {
            GoRESTApiUserInfo goRESTApiUserInfo = goRESTUsers.FirstOrDefault(u => u.email == myUser.Email);
            if (goRESTApiUserInfo != null)
            {
                myUser.GoRESTGender = goRESTApiUserInfo.gender;
                myUser.GoRESTStatus = goRESTApiUserInfo.status;
            }
        }

        private async Task<List<GoRESTApiUserInfo>> GoRESTUsers()
        {
            return await goRestApiService.GetUsers();
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return users;
        }

        [HttpGet("{id:Guid}")]
        public ActionResult<User> GetById(Guid id)
        {
            return Ok(users.FirstOrDefault(u => u.Id == id));
        }

        [HttpGet("{email}")]
        public ActionResult<User> GetByEmail(string email)
        {
            return Ok(users.FirstOrDefault(u => u.Email == email));
        }

        [HttpPost]
        public ActionResult<User> Insert([FromBody] User user)
        {
            user.Id = Guid.NewGuid(); //Typically this would be generated by DB
            users.Add(user);
            return user;
        }

        [HttpPut("{id:Guid}")]
        public ActionResult<User> Replace(Guid id, [FromBody] User user)
        {
            User oldUser = users.FirstOrDefault(u => u.Id == id);
            if (oldUser == null) return NotFound();
            oldUser = user;
            return users.FirstOrDefault(u => u.Id == id);
        }

        [HttpPatch("{id:Guid}")]
        public ActionResult<User> Update(Guid id, [FromBody] User user)
        {
            User oldUser = users.FirstOrDefault(u => u.Id == id);
            if (oldUser == null) return NotFound();
            /* A true PATCH call is outside the scope of this practice API, but if
               we were to do it, then we would parse the JSON object
               to know which properties need to be updated; (via JToken or thirdparty
               JSON patch modeler, etc.)                             
             */
            oldUser.Name = user.Name; //Just for an example of a PATCH endpoint we'll assume the request contained just the Name property (See comment above)
            return users.FirstOrDefault(u => u.Id == id);
        }
    }
}
