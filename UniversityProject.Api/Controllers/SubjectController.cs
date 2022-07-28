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
    [Authorize]
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
        public IActionResult GetAll()
        {
            ApiResponse<IEnumerable<SubjectDto>> response = default;
            try
            {
                IEnumerable<SubjectDto> subjects = _subjectService.GetAllSubject();
                 response = new ApiResponse<IEnumerable<SubjectDto>>
                    (
                        Convert.ToInt32(subjects != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        subjects
                    );
               
            }
            catch (Exception exception)
            {
                response = new ApiResponse<IEnumerable<SubjectDto>>
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
        /// Retrive one subject
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetByIdSubject(int id)
        {
            ApiResponse<SubjectDto> response = default;
            try 
            {
                SubjectDto subject = await _subjectService.GetById(id);
                response = new ApiResponse<SubjectDto>
                    (
                        Convert.ToInt32(subject != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        subject
                    );
                
            }
            catch (Exception exception)
            {
                response = new ApiResponse<SubjectDto>
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
        /// Retrive all subject by student ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("Student/{id:int}")]
        public IActionResult GetAllBySubject([FromRoute] int id)
        {
            ApiResponse<IEnumerable<DetailsStudentDto>> response = default;
            try
            {
                DetailsSubject details = new DetailsSubject();
                details.IdStudent = id;
                IEnumerable<DetailsStudentDto> subject = _subjectService.GetAllByStudent(details);
                response = new ApiResponse<IEnumerable<DetailsStudentDto>>
                    (
                        Convert.ToInt32(subject != null ? HttpStatusCode.OK : HttpStatusCode.NoContent),
                        "",
                        false,
                        subject
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
        /// Add the subjects
        /// </summary>
        /// <param name="subjectDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddSubject(SubjectDto subjectDto)
        {
            ApiResponse<Subject> response = default;
            try
            {
                 Subject subject = await _subjectService.Add(subjectDto);
                response = new ApiResponse<Subject>
                    (
                       Convert.ToInt32(subject != null ? HttpStatusCode.OK : HttpStatusCode.NoContent),
                        "",
                        false,
                        subject
                    );
               
            }
            catch (Exception exception)
            {
                response = new ApiResponse<Subject>
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
        /// Update subject by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="subjectDto"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateById(int id, SubjectDto subjectDto)
        {
            ApiResponse<bool> response = default;
            try
            {
                bool subject = await _subjectService.UpdateById(id, subjectDto);
                response = new ApiResponse<bool>
                    (
                        Convert.ToInt32(subject != false ? HttpStatusCode.OK : HttpStatusCode.NoContent),
                        "",
                        false,
                        subject
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
            return Ok(response);
        }

        /// <summary>
        /// Delete subject by subject ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [Authorize(Policy = nameof(Roles.Administrator))]
        public async Task<IActionResult> DeleteById(int id)
        {
            ApiResponse<bool> response = default;
            try
            {
                bool subject = await _subjectService.DeleteById(id);
                response = new ApiResponse<bool>
                    (
                         Convert.ToInt32(subject != false ? HttpStatusCode.OK : HttpStatusCode.NoContent),
                        "",
                        false,
                        subject
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
            return Ok(response);
        }
    }
}
