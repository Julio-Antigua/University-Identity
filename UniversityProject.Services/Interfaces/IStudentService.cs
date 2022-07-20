using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Services.Interfaces
{
    public interface IStudentService
    {
        PagedList<Student> GetAll(StudentQueryFilter filter);
        Task<StudentDto> GetById(int id);
        Task Add(StudentDto studentDto);
        Task<bool> UpdateById(int id,StudentDto studentDto);
        Task<bool> DeleteById(int id);
        Task<IEnumerable<DetailsStudentDto>> GetAllBySubject(DetailsSubject details);
        //Task GetAllBySubject(int id);
    }
}
