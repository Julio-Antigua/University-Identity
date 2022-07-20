using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<SubjectDto> subjects = _subjectService.GetAll();
            ApiResponse<IEnumerable<SubjectDto>> response = new ApiResponse<IEnumerable<SubjectDto>>(subjects);
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdSubject(int id)
        {
            SubjectDto subejct = await _subjectService.GetById(id);
            ApiResponse<SubjectDto> response = new ApiResponse<SubjectDto>(subejct);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectDto subject)
        {
            await _subjectService.Add(subject);
            ApiResponse<SubjectDto> response = new ApiResponse<SubjectDto>(subject);
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById(int id, SubjectDto subject)
        {
            bool subejct = await _subjectService.UpdateById(id,subject);
            ApiResponse<bool> response = new ApiResponse<bool>(subejct);
            return Ok(response);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteById(int id)
        {
            bool subejct = await _subjectService.DeleteById(id);
            ApiResponse<bool> response = new ApiResponse<bool>(subejct);
            return Ok(response);
        }
    }
}
