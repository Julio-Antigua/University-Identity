using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using UniversityProject.Infrastructure.Filters;

namespace UniversityProject.Services.Extensions
{
    public static class ControllerExtension
    {
        public static IServiceCollection AddControllersConfig(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
                options.Filters.Add<GlobalValidationFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            }).AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));
            //.ConfigureApiBehaviorOptions(options => { /*options.SuppressModelStateInvalidFilter = true;*/ });

            return services;
        }
    }
}
