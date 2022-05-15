using IdentityService.API.Contracts.Responses.Base;
using IdentityService.API.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers.Base
{
    public class BaseController : ControllerBase
    {
        public async Task<TResponse> ExecuteAsync<TResponse>(Func<TResponse, Task<TResponse>> func) where TResponse : ResponseBase, new()
        {
            TResponse response = new();
            try
            {
                response.IsSuccessed = true;

                return await func(response);
            }
            catch (Exception ex)
            {
                response.IsSuccessed = false;
                response.ErrorMessage = ex is BusinessException ? ex.Message : "A general error occured.";
            }

            return await Task.FromResult(response);
        }
    }
}
