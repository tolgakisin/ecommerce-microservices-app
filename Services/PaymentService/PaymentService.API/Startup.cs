using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PaymentService.API.Extensions;
using PaymentService.API.IntegrationEvents.EventHandlers;
using PaymentService.API.IntegrationEvents.Events;
using Polly;
using System;

namespace PaymentService.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureConsul(Configuration);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PaymentService.API", Version = "v1" });
            });

            services.AddSingleton<IRabbitMQBase, RabbitMQBase>();
            services.AddSingleton<IEventManager, EventManager>();

            services.AddScoped<IEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventManager eventManager, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PaymentService.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RegisterWithConsul(lifetime, Configuration);

            Policy.Handle<Exception>().WaitAndRetryForever(_ => TimeSpan.FromSeconds(5)).Execute(() =>
            {
                eventManager.Subscribe<OrderCreatedEvent, OrderCreatedEventHandler>();
            });
        }
    }
}
