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

        public AuthService(UserManager<IdentityUser> userManager,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task Login(LoginDto login)
        {
            IdentityUser user = await _userManager.FindByNameAsync(login.UserName);
            if (user == null)
                throw new BusinessException("This user doesn't exits.");
            SignInResult signInResult = await _signInManager.PasswordSignInAsync(login.UserName,login.Password,false,false);
            if (!signInResult.Succeeded)
                throw new BusinessException("There was an error.");
        }

        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task RegisterRole(RoleDto role)
        {
            bool roleExist = await _roleManager.RoleExistsAsync(role.Name);
            if (roleExist)
                throw new BusinessException("the role already exits");
            IdentityResult result = await _roleManager.CreateAsync(new IdentityRole { Name = role.Name });
            if (!result.Succeeded)
                throw new BusinessException(result.Errors.ToString());
        }

        public async Task UpdateRole(string userName, string oldRole, string newRole)
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
            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, oldRole);

            if (!result.Succeeded)
            {
                throw new BusinessException("This user not in role");
            }
            bool roleNew = await _roleManager.RoleExistsAsync(newRole);
            if (roleNew == false)
            {
                await _userManager.AddToRoleAsync(user, oldRole);
                throw new BusinessException("The newRole doesn't exists");     
            }
            await _userManager.AddToRoleAsync(user, newRole);
        }

        public async Task SignIn(RegisterDto register)
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
            IdentityResult userResult = await _userManager.CreateAsync(user,register.Password);
            if (!userResult.Succeeded)
                throw new BusinessException(userResult.Errors.ToString());
            IdentityResult roleResult = await _userManager.AddToRoleAsync(user,nameof(Roles.Student));
            if (!roleResult.Succeeded)
                throw new BusinessException(userResult.Errors.ToString());
            await Login(new LoginDto { UserName = register.UserName, Password = register.Password });
        }
    }
}
