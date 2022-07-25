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
        public DetailsSubjectRepository(UniversityContext context) : base(context){}

        public IEnumerable<DetailsSubject> GetStudentBySubject(int id)
        {
            IEnumerable<DetailsSubject> details = _entities.Where(x => x.IdSubject == id).ToList();
            if (!details.Any(x => x.IdSubject == id))
            {
                throw new BusinessException("this subject does not exist");
            }
            return details; 
        }

        public IEnumerable<DetailsSubject> GetSubjectByStudent(int id)
        {
            IEnumerable<DetailsSubject> details = _entities.Where(x => x.IdStudent == id).ToList();
            if (!details.Any(x => x.IdStudent == id))
            {
                throw new BusinessException("this student does not exist");
            }
            return details;
        }

        public async Task DeleteByIdStudent(int idStudent, int idSubject)
        {
            DetailsSubject student = await _entities.Where(x => x.IdStudent == idStudent && x.IdSubject == idSubject).FirstOrDefaultAsync();
            if (student == null)
            {
                throw new BusinessException("this student does not exist");
            }
            _entities.Remove(student);
        }

    }
}
