using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task<SignInResult> Login(LoginDto login);
        Task<bool> LogOut();
        Task<IdentityResult> RegisterRole(RoleDto role);
        Task<IdentityResult> UpdateRole(string userId, string oldRole, string newRole);
        Task<SignInResult> SignIn(RegisterDto register);
     }
}
