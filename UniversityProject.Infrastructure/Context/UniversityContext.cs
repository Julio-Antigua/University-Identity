using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniversityProject.Domain.Entities;

#nullable disable

namespace UniversityProject.Infrastructure.Context
{
    public partial class UniversityContext : IdentityDbContext
    {
        public UniversityContext()
        {
        }

        public UniversityContext(DbContextOptions<UniversityContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<DetailsSubject> DetailsSubjects { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Subject> Subjects { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LPT3FWS793;Initial Catalog=University;Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
