using Orchestrator.Saga.Models;
using System.Threading.Tasks;

namespace Orchestrator.RabbitMQ
{
    public interface IRabbitBus
    {
        Task<T> SendMessageAsync<T>(T message) where T : SagaModel;
        void CloseChannel();
    }
}