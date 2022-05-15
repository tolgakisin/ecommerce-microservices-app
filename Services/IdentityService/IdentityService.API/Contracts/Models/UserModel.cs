using IdentityService.API.Contracts.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Models
{
    public class UserModel : BaseModel
    {
        public string Email { get; set; }
    }
}
