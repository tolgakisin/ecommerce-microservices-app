using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Common.Exceptions
{
    public class BusinessException : System.Exception
    {
        public BusinessException(string message) : base(message)
        {

        }
    }
}
