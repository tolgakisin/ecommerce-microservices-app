using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using ProductService.Command.Data.Common;
using ProductService.Query.Consumers;
using ProductService.Query.Data.Common;
using System.Reflection;

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
                    cfg.Host("localhost", 5673, "/", c =>
                    {
                        c.Username("test");
                        c.Password("test");
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
