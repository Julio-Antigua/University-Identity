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
    public class SubjectController : ControllerBase
    {
        private readonly ISubjectService _subjectService;

        public SubjectController(ISubjectService repository)
        {
            this._subjectService = repository;
        }
         /// <summary>
         /// Retrive all subject
         /// </summary>
         /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<SubjectDto> subjects = _subjectService.GetAll();
            ApiResponse<IEnumerable<SubjectDto>> response = new ApiResponse<IEnumerable<SubjectDto>>(subjects);
            return Ok(response);
        }

        /// <summary>
        /// Retrive one subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdSubject(int id)
        {
            SubjectDto subejct = await _subjectService.GetById(id);
            ApiResponse<SubjectDto> response = new ApiResponse<SubjectDto>(subejct);
            return Ok(response);
        }

        /// <summary>
        /// Retrive all subject by student ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Student/{id:int}")]
        public async Task<IActionResult> GetAllBySubject([FromRoute] int id)
        {
            DetailsSubject details = new DetailsSubject();
            details.IdStudent = id;
            var student = await _subjectService.GetAllBySubject(details);
            ApiResponse<IEnumerable<DetailsStudentDto>> response = new ApiResponse<IEnumerable<DetailsStudentDto>>(student);
            return Ok(response);
        }

        /// <summary>
        /// Add the subjects
        /// </summary>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectDto subject)
        {
            await _subjectService.Add(subject);
            ApiResponse<SubjectDto> response = new ApiResponse<SubjectDto>(subject);
            return Ok(response);
        }

        /// <summary>
        /// Update subject by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subject"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById(int id, SubjectDto subject)
        {
            bool subejct = await _subjectService.UpdateById(id,subject);
            ApiResponse<bool> response = new ApiResponse<bool>(subejct);
            return Ok(response);
        }

        /// <summary>
        /// Delete subject by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool subejct = await _subjectService.DeleteById(id);
            ApiResponse<bool> response = new ApiResponse<bool>(subejct);
            return Ok(response);
        }
    }
}
