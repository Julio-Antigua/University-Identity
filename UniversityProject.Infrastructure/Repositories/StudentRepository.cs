using System.Collections.Generic;
using System.Linq;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;

namespace UniversityProject.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(UniversityContext _context) : base(_context) { }

        public IEnumerable<DetailsStudent> GetAllBySubject(DetailsSubject details)
        {
            IQueryable<DetailsStudent> getStudent = from student in _context.Students
                                                       join detail in _context.DetailsSubjects
                                                       on student.Id equals detail.IdStudent
                                                       join subject in _context.Subjects on detail.IdSubject equals subject.Id
                                                       where subject.Id == details.IdSubject
                                                       select new DetailsStudent
                                                       {
                                                           IdStudent = student.Id,
                                                           FirstName = student.FirstName,
                                                           LastName = student.LastName,
                                                           Email = student.Email,
                                                           Subject = subject.Name,
                                                           IdSubject = subject.Id
                                                       };

            if (!getStudent.Any(x => x.IdSubject == details.IdSubject))
            {
                throw new BusinessException("this subject does not exist");
            }

            return getStudent;
        }

        public IEnumerable<DetailsStudent> GetAllByStudent(DetailsSubject details)
        {
            IQueryable<DetailsStudent> getSubject = from subject in _context.Subjects
                                                       join detail in _context.DetailsSubjects
                                                       on subject.Id equals detail.IdSubject
                                                       join student in _context.Students
                                                       on detail.IdStudent equals student.Id
                                                       where student.Id == details.IdStudent
                                                       select new DetailsStudent
                                                       {
                                                           IdStudent = student.Id,
                                                           FirstName = student.FirstName,
                                                           LastName = student.LastName,
                                                           Email = student.Email,
                                                           Subject = subject.Name,
                                                           IdSubject = subject.Id
                                                       };
            if (!getSubject.Any(x => x.IdStudent == details.IdStudent))
            {
                throw new BusinessException("this subject does not exist");
            }
            return getSubject;
        }
    }
}
