using System;
using System.Collections.Generic;
using StampinUp.Service.Models;

namespace StampinUp.Service.Services
{
    public class UsersService : IUsersService
    {
        public List<User> GetAllUsers() => throw new NotImplementedException();
        public User GetById(Guid id) => throw new NotImplementedException();
        public User GetByEmail(string email) => throw new NotImplementedException();
        public User Insert(User user) => throw new NotImplementedException();
        public User Replace(Guid id, User user) => throw new NotImplementedException();
        public User Update(Guid id, User user) => throw new NotImplementedException();
    }
}
