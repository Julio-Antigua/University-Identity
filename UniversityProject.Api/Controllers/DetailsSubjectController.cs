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
    public class DetailsSubjectController : ControllerBase
    {
        private readonly IDetailsSubjectService _service;

        public DetailsSubjectController(IDetailsSubjectService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           IEnumerable<DetailsSubjectDto> details = _service.GetAll();
           ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
           return Ok(response);
        }

        [HttpGet("student/{id:int}")]
        public async Task<IActionResult> GetByIdStudent(int id)
        {
            IEnumerable<DetailsSubjectDto> details = _service.GetByIdStudent(id);
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
            return Ok(response);
        }

        [HttpGet("subject/{id:int}")]
        public async Task<IActionResult> GetByIdSubject(int id)
        {
            IEnumerable<DetailsSubjectDto> details = _service.GetByIdSubject(id);
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteByIdStudent(int id)
        {
            bool details = await _service.DeleteByIdStudent(id);
            ApiResponse<bool> response = new ApiResponse<bool>(details);
            return Ok(response);
        }
    }
}
