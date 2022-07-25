using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace UniversityProject.Services.Extensions
{
    public static class CorsExtension
    {
        public static IServiceCollection AddCorsConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowCredentials();
                });
            });
            return services;
        }
    }
}
