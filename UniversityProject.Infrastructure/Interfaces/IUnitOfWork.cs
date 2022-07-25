using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository StudentRepository { get; }
        IBaseRepository<Subject> SubjectRepository { get; }
        IDetailsSubjectRepository DetailsSubjectRepository { get; }
        IBaseRepository<Course> CourseRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();
    }
}
