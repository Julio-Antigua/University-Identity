using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;

namespace UniversityProject.Infrastructure.Interfaces
{
    public interface IDetailsSubjectRepository : IRepository<DetailsSubject>
    {
        IEnumerable<DetailsSubject> GetStudentBySubject(int id);
        IEnumerable<DetailsSubject> GetSubjectByStudent(int id);
        Task DeleteByIdStudent(int idStudent,int idSubject);
    }
}
