using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using StampinUp.Service.Models;
using StampinUp.Service.Services;

namespace StampinUp.Service.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;            
        }
                
        [HttpGet]
        public IActionResult Get()
        {
            List<User> users = usersService.GetAllUsers();
            return Ok(users);
        }

        [HttpGet("{id:Guid}")]
        public IActionResult Get(Guid id)
        {
            User user = usersService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{email}")]
        public IActionResult Get(string email)
        {
            User user = usersService.GetByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public IActionResult Insert([FromBody] User newUser)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            User user = usersService.Insert(newUser);
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPut("{id:Guid}")]
        public IActionResult Replace(Guid id, [FromBody] User user)
        {
            User newUser = usersService.Replace(id, user);
            if (newUser == null)
            {
                return NotFound();
            }
            return CreatedAtAction("Get", new { id = user.Id }, user);
        }

        [HttpPatch("{id:Guid}")]
        public IActionResult Update(Guid id, [FromBody] User user)
        {
            User newUser = usersService.Update(id, user);
            if (newUser == null)
            {
                return NotFound();
            }
            return CreatedAtAction("Get", new { id = user.Id }, newUser);
        }
    }
}
