using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IInsurancesService
    {
        Task<InsuranceDTO> AddAsync(InsuranceCreateDTO dtoc);
        Task DeleteAsync(InsuranceModel model);
        Task<APIResponse<InsuranceDTO>> GetAllAsync(InsuranceRequest request);
        Task<InsuranceModel> GetByIdAsync(int id);
        Task UpdateAsync(InsuranceUpdateDTO dtou);
    }
}