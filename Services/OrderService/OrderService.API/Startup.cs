using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OrderService.API.Extensions;
using OrderService.Application.IntegrationEvents.EventHandlers;
using OrderService.Application.IntegrationEvents.Events;

namespace OrderService.API
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
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderService.API", Version = "v1" });
            });

            ConfigureExtensions(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventManager eventManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            eventManager.Subscribe<PaymentSuccessEvent, PaymentSuccessEventHandler>();
            eventManager.Subscribe<PaymentFailedEvent, PaymentFailedEventHandler>();
            eventManager.Subscribe<OrderStartedEvent, OrderStartedEventHandler>();
            eventManager.Subscribe<OrderSubmittedEvent, OrderSubmittedEventHandler>();
        }

        private void ConfigureExtensions(IServiceCollection services)
        {
            //Auth
            services.ConfigureAuth(Configuration);

            // RabbitMQ
            services.AddSingleton<IRabbitMQBase, RabbitMQBase>();
            services.AddSingleton<IEventManager, EventManager>();

            services.AddScoped<IEventHandler<OrderStartedEvent>, OrderStartedEventHandler>();
            services.AddScoped<IEventHandler<PaymentSuccessEvent>, PaymentSuccessEventHandler>();
        }
    }
}
