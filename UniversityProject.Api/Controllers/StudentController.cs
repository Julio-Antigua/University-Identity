using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.CustomEntities;
using UniversityProject.Domain.Entities;
using UniversityProject.Infrastructure.Interfaces;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using UniversityProject.Services.QueryFilters;

namespace UniversityProject.Api.Controllers
{
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
            PagedList<Student> students = _studentService.GetAll(filter);
            IEnumerable<StudentDto> studentDto = _mapper.Map<IEnumerable<StudentDto>>(students);

            var metadata = new Metadata
            {
                TotalCount = students.TotalCount,
                PageSize = students.PageSize,
                CurrentPage = students.CurrentPage,
                TotalPage = students.TotalPages,
                HasNextPage = students.HasNextPage,
                HasPreviousPage = students.HasPreviousPage,
                NextPageUrl = _uriService.GetStudentPaginationUri(filter, Url.RouteUrl(nameof(GetAll))).ToString(),
                PreviousPageUrl = _uriService.GetStudentPaginationUri(filter, Url.RouteUrl(nameof(GetAll))).ToString()
            };

            ApiResponse<IEnumerable<StudentDto>> response = new ApiResponse<IEnumerable<StudentDto>>(studentDto)
            {
                Meta = metadata
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            StudentDto studentDto = await _studentService.GetById(id);
            ApiResponse<StudentDto> response = new ApiResponse<StudentDto>(studentDto);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Subject/{id:int}")]
        public async Task<IActionResult> GetAllBySubject([FromRoute] int id)
        {
            DetailsSubject details = new DetailsSubject();
            details.IdSubject = id;
            var student = await _studentService.GetAllBySubject(details);
            ApiResponse<IEnumerable<DetailsStudentDto>> response = new ApiResponse<IEnumerable<DetailsStudentDto>>(student);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] StudentDto studentDto)
        { 
            await _studentService.Add(studentDto);
            ApiResponse<StudentDto> response = new ApiResponse<StudentDto>(studentDto);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="studentDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById([FromRoute] int id, [FromBody] StudentDto studentDto)
        {
            bool result = await _studentService.UpdateById(id,studentDto);
            ApiResponse<bool> response = new ApiResponse<bool>(result);
            return Ok(response);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> RemoveById([FromRoute] int id)
        {
            bool result = await _studentService.DeleteById(id);
            ApiResponse<bool> response = new ApiResponse<bool>(result);
            return Ok(response);
        }
    }
}
