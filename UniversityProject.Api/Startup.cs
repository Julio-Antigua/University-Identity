using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Filters;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Infrastructure.Options;
using UniversityProject.Infrastructure.Repositories;
using UniversityProject.Infrastructure.UnitOfWork;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.Services;


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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options => options.Filters.Add<GlobalExceptionFilter>())
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
                })
                .ConfigureApiBehaviorOptions(options => { /*options.SuppressModelStateInvalidFilter = true;*/ });

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));

            services.AddDbContext<UniversityContext>(options => options.UseSqlServer(Configuration.GetConnectionString("sqlConnection")));

            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<ICourseService, CourseService>();
            services.AddTransient<ISubjectService, SubjectService>();
            services.AddTransient<IDetailsSubjectService, DetailsSubjectService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var adsoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(adsoluteUri);
            });

            services.AddMvc(options => { options.Filters.Add<ValidationFilter>(); }).AddFluentValidation(options => options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "UniversityProject.Api", Version = "v1" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlFath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlFath);
            });
            
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
