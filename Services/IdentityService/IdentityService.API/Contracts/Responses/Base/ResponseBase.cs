using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Contracts.Responses.Base
{
    public class ResponseBase
    {
        public bool IsSuccessed { get; set; }
        public string ErrorMessage { get; set; }
    }
}
