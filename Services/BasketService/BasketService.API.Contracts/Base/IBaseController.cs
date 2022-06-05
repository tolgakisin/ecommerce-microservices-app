using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.API.Contracts.Base
{
    public interface IBaseController
    {
        Task<TResponse> ExecuteAsync<TResponse>(Func<TResponse, Task<TResponse>> func) where TResponse : ResponseBase, new();
    }
}
