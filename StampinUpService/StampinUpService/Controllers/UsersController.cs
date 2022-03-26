﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using StampinUp.Service.Models;

namespace StampinUp.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {        
        private readonly List<User> _users = new List<User>();

        public UsersController()
        {
            _users.Add(new User(
                Guid.NewGuid(),
                "supergrover@sesamestreet.com",
                "Super Grover",
                "Crashland",
                "Up Up and Away!",
                new List<UserPlatform>()
                {
                    new UserPlatform(1,"VR Platform",new DateTime(2022, 1, 1, 0, 0, 0)),
                    new UserPlatform(2,"PS5 Platform",new DateTime(2020, 11, 12, 0, 0, 0))
                })
            );

            _users.Add(new User(
                Guid.NewGuid(),
                "oscarthegrouch@sesamestreet.com",
                "Oscar The Grouch",
                "Garbagecanbul",
                "Scram! Get Lost!",
                new List<UserPlatform>())
            );

            _users.Add(new User(
                new Guid("0ba44840-524e-4013-beb0-6340461dcba3"), //Hard code guid for testing GetById,Put,Patch
                "cookiemonster@sesamestreet.com",
                "Cookie Monster",
                "Isle of Cookie",
                "num num num num",
                new List<UserPlatform>()
                {
                    new UserPlatform(1,"Xbox Series S Platform",new DateTime(2020, 11, 10, 0, 0, 0))
                })
            );
        }

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return _users;
        }
    }
}
