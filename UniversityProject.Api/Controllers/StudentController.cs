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
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Api.Controllers
{
    [Authorize]
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
        [HttpGet(Name = nameof(GetAll))]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<IEnumerable<StudentDto>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<IEnumerable<StudentDto>>))]
        public IActionResult GetAll([FromQuery] StudentQueryFilter filter)
        {
            try
            {
                var resultPage = _studentService.GetAll(filter);
                PagedList<Student> students = resultPage.Item1;
                IEnumerable<StudentDto> studentDto = _mapper.Map<IEnumerable<StudentDto>>(students);
                Metadata metadata = resultPage.Item2;
                ApiResponse<IEnumerable<StudentDto>> response = new ApiResponse<IEnumerable<StudentDto>>(studentDto)
                {
                    Meta = metadata
                };
                Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Retrive one student
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            try
            {
                StudentDto studentDto = await _studentService.GetById(id);
                ApiResponse<StudentDto> response = new ApiResponse<StudentDto>(studentDto);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Retrive all student 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Subject/{id:int}")]
        public IActionResult GetAllBySubject([FromRoute] int id)
        {
            try
            {
                DetailsSubject details = new DetailsSubject();
                details.IdSubject = id;
                var student = _studentService.GetAllBySubject(details);
                ApiResponse<IEnumerable<DetailsStudentDto>> response = new ApiResponse<IEnumerable<DetailsStudentDto>>(student);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Add the students
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentDto studentDto)
        {
            try
            {
                await _studentService.Add(studentDto);
                ApiResponse<StudentDto> response = new ApiResponse<StudentDto>(studentDto);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Update the student by id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] StudentDto studentDto)
        {
            try
            {
                bool result = await _studentService.UpdateById(id, studentDto);
                ApiResponse<bool> response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Delete the student by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveById([FromRoute] int id)
        {
            try
            {
                bool result = await _studentService.DeleteById(id);
                ApiResponse<bool> response = new ApiResponse<bool>(result);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }
    }
}
