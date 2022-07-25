using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Infrastructure.Repositories;

namespace UniversityProject.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversityContext _context;

        private readonly IStudentRepository _studentRepository;
        private readonly IBaseRepository<Course> _courseRepository;
        private readonly IBaseRepository<Subject> _subjectRepository;
        private readonly IDetailsSubjectRepository _detailsSubjectRepository;

        public UnitOfWork(UniversityContext context, IStudentRepository studentRepository, IBaseRepository<Course> courseRepository, IBaseRepository<Subject> subjectRepository, IDetailsSubjectRepository detailsSubjectRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _detailsSubjectRepository = detailsSubjectRepository;
        }
        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(_context);
        public IBaseRepository<Subject> SubjectRepository => _subjectRepository ?? new BaseRepository<Subject>(_context);
        public IBaseRepository<Course> CourseRepository => _courseRepository ?? new BaseRepository<Course>(_context);
        public IDetailsSubjectRepository DetailsSubjectRepository => _detailsSubjectRepository ?? new DetailsSubjectRepository(_context);

        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
