using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.JsonPatch;

namespace MediAgenda.Application.Interfaces
{
    public interface IReasonsService
    {
        Task<ReasonDTO> AddAsync(ReasonCreateDTO dtoc);
        Task DeleteAsync(ReasonModel model);
        Task<APIResponse<ReasonDTO>> GetAllAsync(ReasonRequest request);
        Task<ReasonModel> GetByIdAsync(int id);
        Task UpdateAsync(ReasonUpdateDTO dtou);
        Task<FluentValidation.Results.ValidationResult> PatchAsync(ReasonModel model, JsonPatchDocument<ReasonPatchDTO> dtop);
    }
}