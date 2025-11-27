using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IDayAvailablesService
    {
        Task<DayAvailableDTO> AddAsync(DayAvailableCreateDTO dtoc);
        Task DeleteAsync(DayAvailableModel model);
        Task<APIResponse<DayAvailableDTO>> GetAllAsync(DayAvailableRequest request);
        Task<DayAvailableModel> GetByIdAsync(int id);
        Task UpdateAsync(DayAvailableUpdateDTO dtou);
    }
}