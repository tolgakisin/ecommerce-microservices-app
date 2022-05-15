using IdentityService.API.Contracts.Models;
using IdentityService.API.Contracts.Responses.Base;
using IdentityService.API.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Responses
{
    public class LoginResponse : BaseResponse<User, UserModel>
    {
        public string Token { get; set; }
    }
}
