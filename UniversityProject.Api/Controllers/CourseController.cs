using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Api.Controllers
{

    [Authorize(Policy = nameof(Roles.Administrator))]
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
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            try 
            {
                IEnumerable<CourseDto> courseList = _courseService.GetAllCourse();
                ApiResponse<IEnumerable<CourseDto>> response = new ApiResponse<IEnumerable<CourseDto>>(courseList);
                return Ok(response);
            } 
            catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Retrive one course 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                CourseDto courseList = await _courseService.GetById(id);
                ApiResponse<CourseDto> response = new ApiResponse<CourseDto>(courseList);
                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
          
        }

        /// <summary>
        /// Add the students
        /// </summary>
        /// <param name="courseDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(CourseDto courseDto)
        {
            try
            {
                await _courseService.Add(courseDto);
                ApiResponse<CourseDto> response = new ApiResponse<CourseDto>(courseDto);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        
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
            try
            {
                bool course = await _courseService.UpdateById(id, courseDto);
                ApiResponse<bool> response = new ApiResponse<bool>(course);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
           
        }

        /// <summary>
        /// Delete course by course ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            try
            {
                bool course = await _courseService.DeleteById(id);
                ApiResponse<bool> response = new ApiResponse<bool>(course);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
         
        }

    }
}
