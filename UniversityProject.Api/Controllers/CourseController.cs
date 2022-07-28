using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.Entities;
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
            ApiResponse<IEnumerable<CourseDto>> response = default;
            try 
            {
                IEnumerable<CourseDto> courseList = _courseService.GetAllCourse();
                response = new ApiResponse<IEnumerable<CourseDto>>
                    (   Convert.ToInt32(courseList != null ? HttpStatusCode.OK : HttpStatusCode.NotFound ),
                        "",
                        false,
                        courseList
                    );
                
            } 
            catch (Exception exception) 
            {
                response = new ApiResponse<IEnumerable<CourseDto>>
                      (
                          Convert.ToInt32(HttpStatusCode.BadRequest),
                          exception.Message.ToString(),
                          true,
                          null
                      );
            }
            return Ok(response);

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
            ApiResponse<CourseDto> response = default;
            try
            {
                CourseDto courseList = await _courseService.GetById(id);
                 response = new ApiResponse<CourseDto>
                    (   
                        Convert.ToInt32(courseList != null ? HttpStatusCode.OK : HttpStatusCode.NotFound ),
                        "",
                        false,
                        courseList
                    );          
            }
            catch(Exception exception)
            {
                response = new ApiResponse<CourseDto>
                     (
                         Convert.ToInt32(HttpStatusCode.BadRequest),
                         exception.Message.ToString(),
                         true,
                         null
                     );
            }
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
            ApiResponse<Course> response = default;
            try
            {
                Course course = await _courseService.Add(courseDto);
                response = new ApiResponse<Course>
                    (
                        Convert.ToInt32(course != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        course
                    );   
            }
            catch (Exception exception)
            {
                response = new ApiResponse<Course>
                     (
                         Convert.ToInt32(HttpStatusCode.BadRequest),
                         exception.Message.ToString(),
                         true,
                         null
                     );
            }
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
            ApiResponse<bool> response = default;
            try
            {
                bool course = await _courseService.UpdateById(id, courseDto);
                response = new ApiResponse<bool>
                    (
                        Convert.ToInt32(course != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                        "",
                        false,
                        course
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<bool>
                      (
                          Convert.ToInt32(HttpStatusCode.BadRequest),
                          exception.Message.ToString(),
                          true,
                          false
                      );
            }
            return NoContent();
        }

        /// <summary>
        /// Delete course by course ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            ApiResponse<bool> response = default;
            try
            {
                bool course = await _courseService.DeleteById(id);
                response = new ApiResponse<bool>
                    (
                        Convert.ToInt32(course != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                        "",
                        false,
                        course
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<bool>
                     (
                         Convert.ToInt32(HttpStatusCode.BadRequest),
                         exception.Message.ToString(),
                         true,
                         false
                     );
            }
            return NoContent();
        }

    }
}
