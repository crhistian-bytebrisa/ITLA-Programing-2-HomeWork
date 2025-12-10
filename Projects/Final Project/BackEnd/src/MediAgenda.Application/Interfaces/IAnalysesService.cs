using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IAnalysesService
    {
        Task<AnalysisDTO> AddAsync(AnalysisCreateDTO dtoc);
        Task DeleteAsync(AnalysisModel model);
        Task<APIResponse<AnalysisDTO>> GetAllAsync(AnalysisRequest request);
        Task<AnalysisModel> GetByIdAsync(int id);
        Task UpdateAsync(AnalysisUpdateDTO dtou);
    }
}