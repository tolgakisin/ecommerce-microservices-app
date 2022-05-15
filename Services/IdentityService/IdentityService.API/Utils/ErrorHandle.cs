using IdentityService.API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Utils
{
    public static class ErrorManagement
    {
        public static void HandleError(string errorMessage)
        {
            throw new BusinessException(errorMessage);
        }
    }
}
