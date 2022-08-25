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
                    builder.WithOrigins(Configuration["Clients:Vue80"], Configuration["Clients:Vue81"], Configuration["Clients:Nuxt"])
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           .AllowCredentials();
                           
                });
            });
            return services;
        }
    }
}
