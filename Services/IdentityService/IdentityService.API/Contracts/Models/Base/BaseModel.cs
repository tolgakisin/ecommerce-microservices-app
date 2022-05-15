using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Models.Base
{
    public abstract class BaseModel
    {
        public Guid Id { get; set; }
    }
}
