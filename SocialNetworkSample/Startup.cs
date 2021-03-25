using System;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SocialNetworkSample.Api.Controllers;
using SocialNetworkSample.Data;
using SocialNetworkSample.Data.Abstract;
using SocialNetworkSample.Services.Commands;
using SocialNetworkSample.Services.Contracts.Commands;

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

            //services.AddScoped<ILogger<ClientsController>, Logger<ClientsController>>();
            //services.AddScoped<ILogger<DataContextFactory>, Logger<DataContextFactory>>();


            AppDomain.CurrentDomain.Load(typeof(RegisterClientCommandRequestHandler).Assembly.FullName);
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            var strings = assemblies.Select(x => x.FullName).ToArray();

            services.AddMediatR(assemblies);
            services.AddSingleton<IDataContextFactory, DataContextFactory>();
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