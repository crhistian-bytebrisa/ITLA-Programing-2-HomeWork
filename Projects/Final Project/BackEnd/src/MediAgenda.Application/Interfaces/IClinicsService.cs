using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IClinicsService
    {
        Task<ClinicDTO> AddAsync(ClinicCreateDTO dtoc);
        Task DeleteAsync(ClinicModel model);
        Task<APIResponse<ClinicDTO>> GetAllAsync(ClinicRequest request);
        Task<APIResponse<DayAvailableSimpleDTO>> GetAllDaysAvailableById(int id, ClinicDaysAvailableRequest request);
        Task<ClinicModel> GetByIdAsync(int id);
        Task UpdateAsync(ClinicUpdateDTO dtou);
    }
}