using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IMedicinesService
    {
        Task<MedicineDTO> AddAsync(MedicineCreateDTO dtoc);
        Task DeleteAsync(MedicineModel model);
        Task<APIResponse<MedicineDTO>> GetAllAsync(MedicineRequest request);
        Task<MedicineModel> GetByIdAsync(int id);
        Task UpdateAsync(MedicineUpdateDTO dtou);
    }
}