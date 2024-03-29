using BasketService.API.Extensions;
using BasketService.Business.Contracts.Services;
using BasketService.Business.IntegrationEvents.EventHandlers;
using BasketService.Business.IntegrationEvents.Events;
using BasketService.Data.Contracts.Repositories.Basket;
using BasketService.Data.Repositories.Basket;
using EventBus.RabbitMQ.Models;
using EventBus.RabbitMQ.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using System;

namespace BasketService.API
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
            ConfigureExtensions(services);

            services.AddControllers().AddNewtonsoftJson();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BasketService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventManager eventManager, IHostApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.API v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.RegisterWithConsul(lifetime, Configuration);

            // Subscribe all events.
            //app.UseEventSubscribing();
            Policy.Handle<Exception>().WaitAndRetryForever(_ => TimeSpan.FromSeconds(5)).Execute(() =>
            {
                eventManager.Subscribe<OrderCreatedEvent, OrderCreatedEventHandler>();
            });
        }

        private void ConfigureExtensions(IServiceCollection services)
        {
            services.ConfigureConsul(Configuration);

            services.ConfigureAuth(Configuration);

            services.AddSingleton(x => x.ConfigureRedis(Configuration));

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService.Business.Services.BasketService>();

            // RabbitMQ
            services.AddSingleton<IRabbitMQBase, RabbitMQBase>();
            services.AddSingleton<IEventManager, EventManager>();

            // Register all EventHandlers.
            //services.AddEventHandlers();

            services.AddScoped<IEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();
        }
    }
}
