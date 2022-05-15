using IdentityService.API.Contracts.Requests;
using IdentityService.API.Contracts.Responses;
using IdentityService.API.Controllers.Base;
using IdentityService.API.Data.Common;
using IdentityService.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        private readonly IIdentityService _identityService;
        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
        {
            return await base.ExecuteAsync<RegisterResponse>(async (response) =>
            {
                var user = await _identityService.RegisterAsync(request.Email, request.Password, request.ConfirmPassword);
                response.SetModels(user);

                return response;
            });
        }

        [HttpPost]
        public async Task<LoginResponse> LoginAsync(LoginRequest request)
        {
            return await base.ExecuteAsync<LoginResponse>(async (response) =>
            {
                var (User, Token) = await _identityService.LoginAsync(request.Email, request.Password);
                response.SetModels(User);
                response.Token = Token;

                return response;
            });
        }
    }
}
