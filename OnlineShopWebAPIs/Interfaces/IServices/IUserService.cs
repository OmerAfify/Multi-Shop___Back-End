using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlineShopWebAPIs.DTOs;

namespace OnlineShopWebAPIs.Interfaces.IServices
{
    public interface IUserService
    {
        public Task<bool> ValidateUser(LoginUserDTO loginUserDTO);
        public Task<string> CreateToken();
    }
}