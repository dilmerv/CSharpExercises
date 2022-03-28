using System;
using System.Collections.Generic;
using StampinUp.Service.Models;

namespace StampinUp.Service.Services
{
    public interface IUsersService
    {
        List<User> GetAllUsers();
        User GetById(Guid id);
        User GetByEmail(string email);
        User Insert(User user);
        User Replace(Guid id, User user);
        User Update(Guid id, User user);
    }
}
