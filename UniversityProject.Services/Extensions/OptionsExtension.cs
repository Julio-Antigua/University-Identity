using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityProject.Domain.Options;

namespace UniversityProject.Services.Extensions
{
    public static class OptionsExtension
    {
        public static IServiceCollection AddOptionsConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            return services;
        }
    }
}
