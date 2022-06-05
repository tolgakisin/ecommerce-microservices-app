using BasketService.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Common.Utils
{
    public static class ErrorManagement
    {
        public static void ThrowError(string message)
        {
            throw new BusinessException(message);
        }
    }
}
