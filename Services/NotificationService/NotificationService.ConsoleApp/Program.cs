using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NotificationService.ConsoleApp.IntegrationEvents.EventHandlers;
using NotificationService.ConsoleApp.IntegrationEvents.Events;
using Polly;
using System;
using System.IO;

namespace NotificationService.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false).Build();

            ServiceProvider serviceProvider = new ServiceCollection()
                                                .AddSingleton<IConfiguration>(configuration)
                                                .AddSingleton<IEventManager, EventManager>()
                                                .AddSingleton<IRabbitMQBase, RabbitMQBase>()
                                                .AddScoped<IEventHandler<PaymentFailedEvent>, PaymentFailedEventHandler>()
                                                .BuildServiceProvider();

            Policy.Handle<Exception>().WaitAndRetryForever(_ => TimeSpan.FromSeconds(5)).Execute(() =>
            {
                var _eventManager = serviceProvider.GetService<IEventManager>();

                _eventManager.Subscribe<PaymentFailedEvent, PaymentFailedEventHandler>();
                _eventManager.Subscribe<PaymentSuccessEvent, PaymentSuccessEventHandler>();
            });
            
            Console.WriteLine("Hello World!");

            Console.ReadKey();
        }
    }
}
