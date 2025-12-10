using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IPermissionsService
    {
        Task<PermissionDTO> AddAsync(PermissionCreateDTO dtoc);
        Task DeleteAsync(PermissionModel model);
        Task<APIResponse<PermissionDTO>> GetAllAsync(PermissionRequest request);
        Task<PermissionModel> GetByIdAsync(int id);
        Task UpdateAsync(PermissionUpdateDTO dtou);
    }
}