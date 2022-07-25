using System.Threading.Tasks;
using UniversityProject.Services.DTOs;

namespace UniversityProject.Services.Interfaces
{
    public interface IAuthService
    {
        Task Login(LoginDto login);
        Task LogOut();
        Task RegisterRole(RoleDto role);
        Task UpdateRole(string userId, string oldRole, string newRole);
        Task SignIn(RegisterDto register);
     }
}
