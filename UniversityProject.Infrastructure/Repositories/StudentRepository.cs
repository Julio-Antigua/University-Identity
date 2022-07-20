using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;

namespace UniversityProject.Infrastructure.Repositories
{
    public class StudentRepository : BaseRepository<Student>, IStudentRepository
    {
        public StudentRepository(UniversityContext _context) : base(_context) { }
        //public async Task<IEnumerable<Student>> GetAllBySubject(DetailsSubject details)
        //{    //DetailsSubject details = new DetailsSubject();
        //    //IEnumerable<Student> student = await _entities.Select( x => x.DetailsSubjects.Where(c => c.IdSubject == details.IdSubject)).ToListAsync();
        //    return student;
        //}
    }
}
