using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface IDetailsSubjectService
    {
        IEnumerable<DetailsSubjectDto> GetAllDetails();
        IEnumerable<DetailsSubjectDto> GetByIdStudent(int id);
        IEnumerable<DetailsSubjectDto> GetByIdSubject(int id);
        Task<bool> DeleteByIdStudent(int idStudent, int idSubject);
    }
}
