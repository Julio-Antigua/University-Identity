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
    [Authorize]
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
        public IActionResult GetAll()
        {
            try 
            {
                IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetAllDetails();
                ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
                return Ok(response);
            } catch (Exception exception) 
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Retrive all details subject by student ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("student/{id:int}")]
        public IActionResult GetByIdStudent(int id)
        {
            try
            {
                IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetByIdStudent(id);
                ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Retrive all details student by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("subject/{idStudent:int}")]
        public IActionResult GetByIdSubject(int id)
        {
            try
            {
                IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetByIdSubject(id);
                ApiResponse<IEnumerable<DetailsSubjectDto>> response = new ApiResponse<IEnumerable<DetailsSubjectDto>>(details);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        /// <summary>
        /// Delete the student by student ID and subject ID
        /// </summary>
        /// <param name="idStudent"></param>
        /// <param name="idSubject"></param>
        /// <returns></returns>
        [Authorize(Policy = nameof(Roles.Administrator))]
        [HttpDelete("student/{idStudent:int}/{idSubject:int}")]
        public async Task<IActionResult> DeleteByIdStudent(int idStudent, int idSubject)
        {
            try
            {
                bool details = await _serviceDetail.DeleteByIdStudent(idStudent, idSubject);
                ApiResponse<bool> response = new ApiResponse<bool>(details);
                return Ok(response);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }
    }
}
