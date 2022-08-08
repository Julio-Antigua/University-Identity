using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Api.Controllers
{
    [Authorize(Policy = nameof( Roles.Administrator))]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public StudentController(IStudentService studentService, IMapper mapper, IUriService uriService)
        {
            this._studentService= studentService;
            this._mapper = mapper;
            this._uriService = uriService;
        }

        /// <summary>
        /// Retrive  all student
        /// </summary>
        /// <param name="filter">Filters to apply</param>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<StudentDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<StudentDto>>))]
        public IActionResult GetAll([FromQuery] StudentQueryFilter filter)
        {
            ApiResponse<IEnumerable<StudentDto>> response = default;
            try
            {
                var resultPage = _studentService.GetAllStudent(filter);
                PagedList<Student> students = resultPage.Item1;
                IEnumerable<StudentDto> studentDto = _mapper.Map<IEnumerable<StudentDto>>(students);
                Metadata metadata = resultPage.Item2;
                response = new ApiResponse<IEnumerable<StudentDto>>
                    (
                        Convert.ToInt32(studentDto != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        studentDto
                    )
                {
                    Meta = metadata
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<IEnumerable<StudentDto>>
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
        /// Retrive one student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            ApiResponse<StudentDto> response = default;
            try
            {
                StudentDto studentDto = await _studentService.GetById(id);
                response = new ApiResponse<StudentDto>
                    (
                        Convert.ToInt32(studentDto != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        studentDto
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<StudentDto>
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
        /// Retrive all student 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Subject/{id:int}")]
        public IActionResult GetAllBySubject([FromRoute] int id)
        {
            ApiResponse<IEnumerable<DetailsStudentDto>> response = default;
            try
            {
                DetailsSubject details = new DetailsSubject();
                details.IdSubject = id;
                IEnumerable<DetailsStudentDto> student = _studentService.GetAllBySubject(details);
                response = new ApiResponse<IEnumerable<DetailsStudentDto>>
                    (
                         Convert.ToInt32(student != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        student
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<IEnumerable<DetailsStudentDto>>
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
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody] StudentDto studentDto)
        {
            ApiResponse<Student> response = default;
            try
            {
                Student student = await _studentService.Add(studentDto);
                response = new ApiResponse<Student>
                    (
                        Convert.ToInt32(student != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        student
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<Student>
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
        /// Update the student by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] StudentDto studentDto)
        {
            ApiResponse<bool> response = default;
            try
            {
                bool result = await _studentService.UpdateById(id, studentDto);
                response = new ApiResponse<bool>
                    (

                        Convert.ToInt32(result != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                        "",
                        false,
                        result
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
        /// Delete the student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [AllowAnonymous]
        public async Task<IActionResult> RemoveById([FromRoute] int id)
        {
            ApiResponse<bool> response = default;
            try
            {
                bool result = await _studentService.DeleteById(id);
                response = new ApiResponse<bool>
                    (

                        Convert.ToInt32(result != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                        "",
                        false,
                        result
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
