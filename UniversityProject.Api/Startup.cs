using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Filters;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Domain.Options;
using UniversityProject.Infrastructure.Repositories;
using UniversityProject.Infrastructure.UnitOfWork;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.Services;
using UniversityProject.Services.Extensions;

namespace UniversityProject.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthConfig();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersConfig(Configuration);
            services.AddCorsConfig(Configuration);
            services.AddDbContextConfig(Configuration);
            services.AddOptionsConfig(Configuration);
            services.AddServicesConfig(Configuration);
            services.AddSwaggerGenConfig($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");    
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UniversityProject.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
