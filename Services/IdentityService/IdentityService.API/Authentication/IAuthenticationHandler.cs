using IdentityService.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Core.Authentication
{
    public interface IAuthenticationHandler
    {
        string CreateToken(User user);
    }
}
