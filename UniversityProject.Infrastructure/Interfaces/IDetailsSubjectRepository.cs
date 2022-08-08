using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IDetailsSubjectRepository : IBaseRepository<DetailsSubject>
    {
        IEnumerable<DetailsSubject> GetStudentBySubject(int id);
        IEnumerable<DetailsSubject> GetSubjectByStudent(int id);
        Task<bool> DeleteByIdStudent(int idStudent,int idSubject);
    }
}
