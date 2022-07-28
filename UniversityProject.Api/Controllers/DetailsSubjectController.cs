using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net;
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
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = default;
            try 
            {
                IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetAllDetails();
                response = new ApiResponse<IEnumerable<DetailsSubjectDto>>
                    (
                        Convert.ToInt32(details != null ? HttpStatusCode.OK : HttpStatusCode.NotFound ),
                        "",
                        false,
                        details
                    );
                
            } catch (Exception exception) 
            {
                response = new ApiResponse<IEnumerable<DetailsSubjectDto>>
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
        /// Retrive all details subject by student ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("student/{id:int}")]
        public IActionResult GetByIdStudent(int id)
        {
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = default;
            try
            {
                IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetByIdStudent(id);
                response = new ApiResponse<IEnumerable<DetailsSubjectDto>>
                    (
                        Convert.ToInt32(details != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        details
                    );
                return Ok(response);
            }
            catch (Exception exception)
            {
                response = new ApiResponse<IEnumerable<DetailsSubjectDto>>
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
        /// Retrive all details student by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("subject/{idStudent:int}")]
        public IActionResult GetByIdSubject(int id)
        {
            ApiResponse<IEnumerable<DetailsSubjectDto>> response = default;
            try
            {
                IEnumerable<DetailsSubjectDto> details = _serviceDetail.GetByIdSubject(id);
                response = new ApiResponse<IEnumerable<DetailsSubjectDto>>
                    (
                         Convert.ToInt32(details != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        details
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<IEnumerable<DetailsSubjectDto>>
                   (
                       Convert.ToInt32(HttpStatusCode.NotFound),
                       exception.Message.ToString(),
                       true,
                       null
                   );
            }
            return Ok(response);
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
            ApiResponse<bool> response = default;
            try
            {
                bool details = await _serviceDetail.DeleteByIdStudent(idStudent, idSubject);
                response = new ApiResponse<bool>
                    (
                         Convert.ToInt32(details != false ? HttpStatusCode.NoContent : HttpStatusCode.NotFound),
                        "",
                        false,
                        details
                    );          
            }
            catch (Exception exception)
            {
                response = new ApiResponse<bool>
                      (
                           Convert.ToInt32(HttpStatusCode.NotFound),
                          exception.Message.ToString(),
                          true,
                          false
                      );
            }
            return NoContent();
        }
    }
}
