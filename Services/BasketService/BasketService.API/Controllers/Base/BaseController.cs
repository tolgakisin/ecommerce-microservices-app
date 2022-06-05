using BasketService.API.Contracts.Base;
using BasketService.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketService.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase, IBaseController
    {
        public async Task<TResponse> ExecuteAsync<TResponse>(Func<TResponse, Task<TResponse>> func) where TResponse : ResponseBase, new()
        {
            TResponse response = new();
            try
            {
                response.IsSuccessed = true;

                return await func(response);
            }
            catch (System.Exception ex)
            {
                response.IsSuccessed = false;
                response.ErrorMessage = ex is BusinessException ? ex.Message : "A general error occured.";
            }

            return await Task.FromResult(response);
        }
    }
}
