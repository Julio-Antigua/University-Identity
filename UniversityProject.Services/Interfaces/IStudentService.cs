using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Interfaces
{
    public interface IStudentService:IPaginateService<StudentDto>
    {
        (PagedList<StudentDto>,Metadata) GetAllStudent(StudentQueryFilter filter);
        Task<StudentDto> GetById(int id);
        Task<Student> Add(StudentDto studentDto);
        Task<bool> UpdateById(int id,StudentDto studentDto);
        Task<bool> DeleteById(int id);
        IEnumerable<DetailsStudentDto> GetAllBySubject(DetailsSubject details);
    }
}
