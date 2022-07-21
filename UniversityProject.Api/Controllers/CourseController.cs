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
        private readonly ICourseService _courseService;
        private readonly IMapper _mapper;

        public CourseController(ICourseService courseService, IMapper mapper)
        {
            _courseService = courseService;
            this._mapper = mapper;
        }

        /// <summary>
        /// Retrive all course
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            IEnumerable<CourseDto> courseList = _courseService.GetAll();
            ApiResponse<IEnumerable<CourseDto>> response = new ApiResponse<IEnumerable<CourseDto>>(courseList);
            return Ok(response);
        }

        /// <summary>
        /// Retrive one course 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            CourseDto courseList = await _courseService.GetById(id);
            ApiResponse<CourseDto> response = new ApiResponse<CourseDto>(courseList);
            return Ok(response);
        }

        /// <summary>
        /// Add the students
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CourseDto courseDto)
        {
            await _courseService.Add(courseDto);
            ApiResponse<CourseDto> response = new ApiResponse<CourseDto>(courseDto);
            return Ok(response);
        }

        /// <summary>
        /// Update course by course ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById(int id, CourseDto courseDto)
        {
            bool course = await _courseService.UpdateById(id,courseDto);
            ApiResponse<bool> response = new ApiResponse<bool>(course);
            return Ok(response);
        }

        /// <summary>
        /// Delete course by course ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool course = await _courseService.DeleteById(id);
            ApiResponse<bool> response = new ApiResponse<bool>(course);
            return Ok(response);
        }

    }
}
