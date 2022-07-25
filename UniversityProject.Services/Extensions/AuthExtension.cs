using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Infrastructure.Context;

namespace UniversityProject.Services.Extensions
{
    public static class AuthExtension
    {
        public static IServiceCollection AddAuthConfig(this IServiceCollection services)
        {
            services.AddDefaultIdentity<IdentityUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;

                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 0;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;

            }).AddRoles<IdentityRole>().AddEntityFrameworkStores<UniversityContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.Events.OnRedirectToLogin = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

                options.Events.OnRedirectToAccessDenied = context =>
                {
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return Task.CompletedTask;
                };

                options.Cookie.HttpOnly = false;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("Administrator", policy => policy.RequireRole(nameof(Roles.Administrator)));
                options.AddPolicy("Teacher", policy => policy.RequireRole(nameof(Roles.Teacher)));
                options.AddPolicy("Student", policy => policy.RequireRole(nameof(Roles.Student)));
                options.AddPolicy("StudentOrTeacher", policy => policy.RequireRole(nameof(Roles.Student), nameof(Roles.Teacher)));
            });

            return services;
        }
    }
}
