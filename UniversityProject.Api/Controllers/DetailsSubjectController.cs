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
        private readonly IDetailsSubjectService _serviceDetail;

        public DetailsSubjectController(IDetailsSubjectService service)
        {
            _serviceDetail = service;
        }

        /// <summary>
        /// Retrive all DetailsSubject
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
           IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetAll();
           ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
           return Ok(response);
        }

        /// <summary>
        /// Retrive all details subject by student ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("student/{id:int}")]
        public async Task<IActionResult> GetByIdStudent(int id)
        {
            IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetByIdStudent(id);
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
            return Ok(response);
        }

        /// <summary>
        /// Retrive all details student by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("subject/{idStudent:int}")]
        public async Task<IActionResult> GetByIdSubject(int id)
        {
            IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetByIdSubject(id);
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
            return Ok(response);
        }

        /// <summary>
        /// Delete the student by student ID and subject ID
        /// </summary>
        /// <param name="idStudent"></param>
        /// <param name="idSubject"></param>
        /// <returns></returns>
        [HttpDelete("student/{idStudent:int}/{idSubject:int}")]
        public async Task<IActionResult> DeleteByIdStudent(int idStudent, int idSubject)
        {
            bool details = await _serviceDetail.DeleteByIdStudent(idStudent,idSubject);
            ApiResponse<bool> response = new ApiResponse<bool>(details);
            return Ok(response);
        }
    }
}
