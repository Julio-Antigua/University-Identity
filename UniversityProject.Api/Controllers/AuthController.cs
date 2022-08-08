using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using UniversityProject.Api.Responses;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Net;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

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
            ApiResponse<SignInResult> response = default;
            try
            {
                SignInResult result = await _authService.Login(login);
                response = new ApiResponse<SignInResult>
                    (
                        Convert.ToInt32(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        result
                    );
              
            }
            catch (Exception exception)
            {
                response = new ApiResponse<SignInResult>
                     (
                         Convert.ToInt32(HttpStatusCode.NotFound),
                         exception.Message.ToString(),
                         true,
                         null
                     );
            }
            return Ok(response);
            
        }

        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            ApiResponse<bool> response = default;
            try
            {

                bool result = await _authService.LogOut();
                response = new ApiResponse<bool>
                    (
                        Convert.ToInt32(result != false ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        result
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
            return Ok(response);

        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterRole(RoleDto role) 
        {
            ApiResponse<IdentityResult> response = default;
            try
            {
                
                IdentityResult result = await _authService.RegisterRole(role);
                response = new ApiResponse<IdentityResult>
                    (
                        Convert.ToInt32(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        result
                    );

            }
            catch (Exception exception)
            {
                response = new ApiResponse<IdentityResult>
                    (
                        Convert.ToInt32(HttpStatusCode.NotFound),
                        exception.Message.ToString(),
                        true,
                        null
                    );
            }
            return Ok(response);
            
        }

        [Authorize(Policy = nameof(Roles.Administrator))]
        [HttpPost]
        [Route("updateRoleByUser")]
        public async Task<IActionResult> UpdateRoleByUser(string userName, string oldRole, string newRole)
        {
            ApiResponse<IdentityResult> response = default;
            try
            {
                IdentityResult result = await _authService.UpdateRole(userName, oldRole,newRole);
                response = new ApiResponse<IdentityResult>
                    (
                        Convert.ToInt32(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        null
                    );
            }
            catch (Exception exception)
            {
                response = new ApiResponse<IdentityResult>
                   (
                       Convert.ToInt32(HttpStatusCode.NotFound),
                       exception.Message.ToString(),
                       true,
                       null
                   );
            }
            return Ok(response);

        }

        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> SignIn(RegisterDto register)
        {
            ApiResponse<SignInResult> response = default;   
            try
            {
                SignInResult result = await _authService.SignIn(register);
                response = new ApiResponse<SignInResult>
                    (
                        Convert.ToInt32(result != null ? HttpStatusCode.OK : HttpStatusCode.NotFound),
                        "",
                        false,
                        result
                    );
            }
            catch (Exception exception)
            {
                response = new ApiResponse<SignInResult>
                   (
                       Convert.ToInt32(HttpStatusCode.NotFound),
                       exception.Message.ToString(),
                       true,
                       null
                   );
            }
            return Ok(response);
        }
    }
}
