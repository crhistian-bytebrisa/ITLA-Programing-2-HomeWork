using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IApplicationUsersService
    {
        Task<ApplicationUserDTO> AddAsync(ApplicationUserCreateDTO dtoc);
        Task DeleteAsync(ApplicationUserModel model);
        Task<APIResponse<ApplicationUserDTO>> GetAllAsync(ApplicationUserRequest request);
        Task<ApplicationUserModel> GetByIdAsync(Guid id);
        Task UpdateAsync(ApplicationUserUpdateDTO dtou);
    }
}