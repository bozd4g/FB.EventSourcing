using System.Collections.Generic;
using FB.EventSourcing.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FB.EventSourcing.Api.Extensions
{
    public static class SwaggerExtension
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = new AppSettings();
            configuration.GetSection("AppSettings").Bind(settings);

            string title = settings.Swagger.Title;
            string description = settings.Swagger.Description;
            string version = settings.Swagger.Version;
            string scheme = "Bearer";

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
            });
        }
    }
}