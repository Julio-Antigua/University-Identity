using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(LoginDto login) 
        {
            try
            {
                await _authService.Login(login);
                return Ok(new ApiResponse());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try 
            {
                await _authService.LogOut();
                return Ok(new ApiResponse());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterRole(RoleDto role) 
        {
            try
            {
                await _authService.RegisterRole(role);
                return Ok(new ApiResponse());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }

        [Authorize(Policy = nameof(Roles.Administrator))]
        [HttpPost]
        [Route("updateRoleByUser")]
        public async Task<IActionResult> UpdateRoleByUser(string userName, string oldRole, string newRole)
        {
            try
            {
                await _authService.UpdateRole(userName, oldRole,newRole);
                return Ok(new ApiResponse());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }

        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn(RegisterDto register)
        {
            try
            {
                await _authService.SignIn(register);
                return Ok(new ApiResponse());
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
            
        }
    }
}
