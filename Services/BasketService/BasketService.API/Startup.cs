using BasketService.API.Extensions;
using BasketService.Business.Contracts.Services;
using BasketService.Business.IntegrationEvents.EventHandlers;
using BasketService.Business.IntegrationEvents.Events;
using BasketService.Business.IntegrationEvents.Events.EventTest;
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IEventManager eventManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Subscribe all events.
            //app.UseEventSubscribing();

            eventManager.Subscribe<Event1, Event1Handler>();
            eventManager.Subscribe<Event2, Event2Handler>();
            eventManager.Subscribe<OrderCreatedEvent, OrderCreatedEventHandler>();
        }

        private void ConfigureExtensions(IServiceCollection services)
        {
            services.ConfigureAuth(Configuration);

            services.AddSingleton(x => x.ConfigureRedis(Configuration));

            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IBasketService, BasketService.Business.Services.BasketService>();

            // RabbitMQ
            services.AddSingleton<IRabbitMQBase, RabbitMQBase>();
            services.AddSingleton<IEventManager, EventManager>();

            // Register all EventHandlers.
            //services.AddEventHandlers();

            services.AddScoped<IEventHandler<Event1>, Event1Handler>();
            services.AddScoped<IEventHandler<Event2>, Event2Handler>();
            services.AddScoped<IEventHandler<OrderCreatedEvent>, OrderCreatedEventHandler>();
        }
    }
}
