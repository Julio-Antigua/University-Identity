using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.Entities;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _repository;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            this._repository = courseService;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<CourseDto> courseList = _repository.GetAll();
            ApiResponse<IEnumerable<CourseDto>> response = new ApiResponse<IEnumerable<CourseDto>>(courseList);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            CourseDto courseList = await _repository.GetById(id);
            ApiResponse<CourseDto> response = new ApiResponse<CourseDto>(courseList);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseDto courseDto)
        {
            await _repository.Add(courseDto);
            ApiResponse<CourseDto> response = new ApiResponse<CourseDto>(courseDto);
            return Ok(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById(int id, CourseDto courseDto)
        {
            bool course = await _repository.UpdateById(id,courseDto);
            ApiResponse<bool> response = new ApiResponse<bool>(course);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool course = await _repository.DeleteById(id);
            ApiResponse<bool> response = new ApiResponse<bool>(course);
            return Ok(response);
        }

    }
}
