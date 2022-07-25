using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityProject.Infrastructure.Context;

namespace UniversityProject.Services.Extensions
{
    public static class DbContextExtension
    {
        public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<UniversityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));
            return services;
        }
    }
}
