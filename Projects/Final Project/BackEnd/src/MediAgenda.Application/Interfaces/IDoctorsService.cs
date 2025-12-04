using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IDoctorsService
    {
        Task<DoctorDTO> AddAsync(DoctorCreateDTO dtoc);
        Task DeleteAsync(DoctorModel model);
        Task<APIResponse<DoctorDTO>> GetAllAsync(DoctorRequest request);
        Task<DoctorModel> GetByIdAsync(int id);
        Task UpdateAsync(DoctorUpdateDTO dtou);
    }
}