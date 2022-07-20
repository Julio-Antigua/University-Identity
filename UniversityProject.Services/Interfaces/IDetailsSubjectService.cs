using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface IDetailsSubjectService
    {
        IEnumerable<DetailsSubjectDto> GetAll();
        IEnumerable<DetailsSubjectDto> GetByIdStudent(int id);
        IEnumerable<DetailsSubjectDto> GetByIdSubject(int id);
        Task<bool> DeleteByIdStudent(int id);
    }
}
