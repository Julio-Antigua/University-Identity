using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace UniversityProject.Services.Extensions
{
    public static class SwaggerGenExtension
    {
        public static IServiceCollection AddSwaggerGenConfig(this IServiceCollection services, string xmlFile)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversityProject.Api", Version = "v1" });
                var xmlFath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlFath);
            });

            return services;
        }
    }
}
