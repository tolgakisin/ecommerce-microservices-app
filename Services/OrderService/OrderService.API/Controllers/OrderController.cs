using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace OrderService.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        [HttpGet]
        public Task GetOrdersAsync()
        {
            return Task.CompletedTask;
        }

        [HttpGet]
        public Task GetOrderByIdAsync(Guid orderId)
        {
            return Task.CompletedTask;
        }
    }
}
