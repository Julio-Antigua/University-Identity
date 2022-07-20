using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Subject> _subjectRepository;
        private readonly IDetailsSubjectRepository _detailsSubjectRepository;
        //private readonly IRepository<DetailsSubject> _detailsSubjectRepository;
        public UnitOfWork(UniversityContext context)
        {
            this._context = context;
        }
        public IStudentRepository StudentRepository => _studentRepository ?? new StudentRepository(_context);

        public IRepository<Subject> SubjectRepository => _subjectRepository ?? new BaseRepository<Subject>(_context);

        //public IRepository<DetailsSubject> DetailsSubjectRepository => _detailsSubjectRepository ?? new BaseRepository<DetailsSubject>(_context);

        public IRepository<Course> CourseRepository => _courseRepository ?? new BaseRepository<Course>(_context);
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
