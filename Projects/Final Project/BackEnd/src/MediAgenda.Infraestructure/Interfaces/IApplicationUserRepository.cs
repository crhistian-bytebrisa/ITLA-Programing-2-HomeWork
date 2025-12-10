using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Identity;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IApplicationUserRepository : IBaseRepositoryIdString<ApplicationUserModel>
    {
        Task<(List<ApplicationUserModel>, int)> GetAllAsync(ApplicationUserRequest request);
        Task<ApplicationUserModel> GetUserByEmailAsync(string email);
        Task<List<IdentityRole>> GetRolesByUserIdAsync(string userId);
        Task<bool> CheckPasswordAsync(ApplicationUserModel user, string password);
    }
}