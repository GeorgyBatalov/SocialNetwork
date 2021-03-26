using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SocialNetworkSample.Data;
using SocialNetworkSample.Data.Abstract;
using SocialNetworkSample.Services.Commands;

namespace SocialNetworkSample
{
    public class Startup
    {
        private const string ApiVersion = "V1";
        private const string Title = "Social network";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(ApiVersion, new OpenApiInfo
                {
                    Title = Title,
                    Description = "Internal social network",
                    Version = ApiVersion
                });
            });


            AppDomain.CurrentDomain.Load(typeof(RegisterClientCommandRequestHandler).Assembly.FullName);

            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            services.AddMediatR(assemblies);
            services.AddSingleton<IDataContextFactory>(provider =>
            {
                var logger = provider.GetRequiredService<ILogger<DataContextFactory>>();
                // TODO: по заданию настроек не должно быть, но по хорошему такие настройки нужно выносить в файл конфигурации
                const string connectionString = "Data Source=SocialNetworkSample.db";
                return new DataContextFactory(logger, connectionString);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseRouting();

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{ApiVersion}/swagger.json", Title);
                options.RoutePrefix = "";
            });

            //app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}