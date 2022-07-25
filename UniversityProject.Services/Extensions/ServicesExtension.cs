using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Infrastructure.Repositories;
using UniversityProject.Infrastructure.UnitOfWork;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.Services;

namespace UniversityProject.Services.Extensions
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServicesConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IDetailsSubjectService, DetailsSubjectService>();
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            

            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IDetailsSubjectRepository, DetailsSubjectRepository>();
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<IUriService, UriService>();

            return services;
        }
    }
}
