using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.Entities;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface ISubjectService
    {
        IEnumerable<SubjectDto> GetAll();
        Task<SubjectDto> GetById(int id);
        Task Add(SubjectDto subjectDto);
        Task<bool> UpdateById(int id,SubjectDto subjectDto);
        Task<bool> DeleteById(int id);
        Task<IEnumerable<DetailsStudentDto>> GetAllBySubject(DetailsSubject details);
    }
}
