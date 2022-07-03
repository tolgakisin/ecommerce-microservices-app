using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.Controllers
{
    [Route("api/[Controller]/[Action]")]
    [ApiController]
    public class SagaController : ControllerBase
    {
        [HttpGet]
        public string TestEndpoint() => "Test";
    }
}
