using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System;

namespace ProductService.EventBus.RabbitMQ
{
    public class RabbitMQBase : IRabbitMQBase
    {
        private readonly IConfiguration _configuration;
        private ConnectionFactory _connectionFactory;
        private IConnection _connection;

        public RabbitMQBase(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConnection CreateConnection()
        {
            if (_connectionFactory != null && _connection != null && _connection.IsOpen) return _connection;

            _connectionFactory = new ConnectionFactory() { Uri = new Uri(GetConnectionString()) };
            _connection = _connectionFactory.CreateConnection();

            return _connection;
        }

        private string GetConnectionString()
        {
            var rabbitSection = _configuration.GetSection("RabbitMQ");
            var username = rabbitSection.GetSection("username");
            var password = rabbitSection.GetSection("password");
            var host = rabbitSection.GetSection("host");
            var virtualHost = rabbitSection.GetSection("virtualHost");
            string connectionString = "amqp://{0}:{1}@{2}/{3}";

            return string.Format(connectionString, username, password,
                host, virtualHost);
        }
    }
}
