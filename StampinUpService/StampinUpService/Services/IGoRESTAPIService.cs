using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StampinUp.Service.Models;

namespace StampinUp.Service.Services
{
    public interface IGoRESTApiService
    {
        Task<List<GoRESTApiUserInfo>> GetUsers();
    }
}