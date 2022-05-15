using IdentityService.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Services
{
    public interface IIdentityService
    {
        Task<User> RegisterAsync(string email, string password, string confirmPassword);
        Task<(User User, string token)> LoginAsync(string email, string password);
    }
}
