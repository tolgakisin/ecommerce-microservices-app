using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchestrator.RabbitMQ
{
    public class RabbitMQBase : IRabbitMQBase
    {
        private ConnectionFactory _factory;
        private IConnection _connection;
        private readonly IConfiguration _configuration;

        public RabbitMQBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnection CreateBus()
        {
            if (_factory != null && _connection != null && _connection.IsOpen) return _connection;

            _factory = new ConnectionFactory() { Uri = new Uri(GetConnectionString()) };
            _connection = _factory.CreateConnection();

            return _connection;
        }

        private string GetConnectionString()
        {
            string user = _configuration.GetSection("RabbitMQ:Username").Value;
            string password = _configuration.GetSection("RabbitMQ:Password").Value;
            string host = _configuration.GetSection("RabbitMQ:Host").Value;
            string virtualHost = _configuration.GetSection("RabbitMQ:VirtualHost").Value;
            string connectionString = "amqp://{0}:{1}@{2}/{3}";

            return string.Format(connectionString, user, password,
                host, virtualHost);
        }
    }
}
