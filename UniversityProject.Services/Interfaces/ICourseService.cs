using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAllCourse();
        Task<CourseDto> GetById(int id);
        Task Add(CourseDto courseDto);
        Task<bool> UpdateById(int id,CourseDto courseDto);
        Task<bool> DeleteById(int id);
    }
}
