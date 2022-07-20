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
        IRepository<Subject> SubjectRepository { get; }
        IDetailsSubjectRepository DetailsSubjectRepository { get; }
        //IRepository<DetailsSubject> DetailsSubjectRepository { get; }
        IRepository<Course> CourseRepository { get; }
        void SaveChanges();
        Task SaveChangesAsync();

    }
}
