using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Linq;
using Orchestrator.Saga.Models;
using System.Threading.Tasks;

namespace Orchestrator.RabbitMQ
{
    public interface IRabbitBus
    {
        Task<object> SendMessageAsync(object messageObj, SagaModel sagaMessage, IApplicationBuilder app);
        void CloseChannel();
    }
}