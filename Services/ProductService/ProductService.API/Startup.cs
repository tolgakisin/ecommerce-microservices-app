using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.Threading.Tasks;
using MassTransit;
using ProductService.Query.Consumers;
using ProductService.Command.Data.Common;
using Microsoft.EntityFrameworkCore;
using ProductService.Query.Data.Common;

namespace ProductService.API
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
            services.AddMediatR(typeof(ProductDbContext).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(ProductReadDbContext).GetTypeInfo().Assembly);

            services.AddMassTransit((x) =>
            {
                x.AddConsumer<ProductCreatedEventConsumer>(typeof(ProductCreatedEventConsumerDefinition));
                x.AddConsumer<ProductDeletedEventConsumer>(typeof(ProductDeletedEventConsumerDefinition));

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", c =>
                    {
                        c.Username("guest");
                        c.Password("guest");
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            services.AddDbContext<ProductDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionString:ProductServiceWriteDb").Value);
            });

            services.AddDbContext<ProductReadDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetSection("ConnectionString:ProductServiceReadDb").Value);
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductService.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductService.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
