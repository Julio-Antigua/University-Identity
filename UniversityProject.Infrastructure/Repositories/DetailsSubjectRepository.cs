using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Infrastructure.Context;
using UniversityProject.Infrastructure.Interfaces;

namespace UniversityProject.Infrastructure.Repositories
{
    public class DetailsSubjectRepository : BaseRepository<DetailsSubject>, IDetailsSubjectRepository
    {
        public DetailsSubjectRepository(UniversityContext context) : base(context)
        {

        }


        public IEnumerable<DetailsSubject> GetStudentBySubject(int id)
        {
            IEnumerable<DetailsSubject> details = _entities.Where(x => x.IdSubject == id).ToList();
            return details; 
        }

        public IEnumerable<DetailsSubject> GetSubjectByStudent(int id)
        {
            IEnumerable<DetailsSubject> details = _entities.Where(x => x.IdStudent == id).ToList();
            return details;
        }

        public async Task<bool> DeleteByIdStudent(int id)
        {
            DetailsSubject student = await _entities.Where(x => x.IdStudent == id).FirstOrDefaultAsync();
            _entities.Remove(student);
            return true;
        }
    }
}
