using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UniversityProject.Domain.Enumerations;
using UniversityProject.Domain.Exceptions;
using UniversityProject.Services.DTOs;
using UniversityProject.Services.Interfaces;

namespace UniversityProject.Services.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly HttpContext _httpContext;

        public AuthService(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,IHttpContextAccessor httpContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _httpContext = httpContext.HttpContext;
        }

        public async Task<SignInResult> Login(LoginDto login)
        {
            IdentityUser user = await _userManager.FindByNameAsync(login.UserName);
            IdentityUser identity= user == null ? throw new BusinessException("This user doesn't exits.") : user;
            if (user == null)
                throw new BusinessException("This user doesn't exits.");
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(login.UserName,login.Password,false,false);
            if (!signInResult.Succeeded)
                throw new BusinessException("There was an error.");
            return signInResult;
        }

        public async Task<bool> LogOut()
        {
            if (!_httpContext.User.Identity.IsAuthenticated) 
            {
                return false;
            }
            await _signInManager.SignOutAsync();
            return true;
        }

        public async Task<IdentityResult> RegisterRole(RoleDto role)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(role.Name);
            if (roleExist)
                throw new BusinessException("the role already exits");
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole { Name = role.Name });
            if (!result.Succeeded)
            {
                var response = "";
                foreach (var item in result.Errors)
                {
                    response = item.Description;
                }
                throw new BusinessException(response);
            }
            return result;
        }

        public async Task<IdentityResult> UpdateRole(string userName, string oldRole, string newRole)
        {
            IdentityUser user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {        
                throw new BusinessException("This userName doesn't exists");
            }

            bool roleOld = await _roleManager.RoleExistsAsync(oldRole);
            if (roleOld == false)
            {
                throw new BusinessException("The oldRole doesn't exists");
            }
            IdentityResult remove = await _userManager.RemoveFromRoleAsync(user, oldRole);

            if (!remove.Succeeded)
            {
                throw new BusinessException("This user not in role");
            }

            bool roleNew = await _roleManager.RoleExistsAsync(newRole);
            if (roleNew == false)
            {
                await _userManager.AddToRoleAsync(user, oldRole);
                throw new BusinessException("The newRole doesn't exists");     
            }
            IdentityResult result = await _userManager.AddToRoleAsync(user, newRole);
            return result;
        }

        public async Task<SignInResult> SignIn(RegisterDto register)
        {
            IdentityUser user = new IdentityUser 
            {
                UserName = register.UserName,
                Email = register.Email,
                EmailConfirmed = true,
                PasswordHash = register.Password,               
            };
            IdentityUser exist = await _userManager.FindByNameAsync(register.UserName);
            if (exist != null)
                throw new BusinessException("This username already exists");
            if(user.Email == "")
            {
                throw new BusinessException("The email field cannot be emptys");
            }
            if (!user.Email.Contains("@") && user.Email.Contains("."))
            {
                throw new BusinessException("The email is invalid");
            }
            IdentityResult userResult = await _userManager.CreateAsync(user,register.Password);
            if (!userResult.Succeeded)
            {
                var response = "";
                 foreach (var item in userResult.Errors)
                {
                    response = item.Description;
                }
                throw new BusinessException(response);
            }
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user,nameof(Roles.Student));
            if (!roleResult.Succeeded)
            {
                var response = "";
                foreach (var item in userResult.Errors)
                {
                    response = item.Description;
                }
                throw new BusinessException(response);
            }
            SignInResult result = await Login(new LoginDto { UserName = register.UserName, Password = register.Password });
            if (!result.Succeeded)
            {
                var response = "";
                foreach (var item in userResult.Errors)
                {
                    response = item.Description;
                }
                throw new BusinessException(response);
            }
            return result;
        }
    }
}
