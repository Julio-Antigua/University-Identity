using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface ICourseService
    {
        IEnumerable<CourseDto> GetAll();
        Task<CourseDto> GetById(int id);
        Task Add(CourseDto courseDto);
        Task<bool> UpdateById(int id,CourseDto courseDto);
        Task<bool> DeleteById(int id);
    }
}
