using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface INotesPatientsService
    {
        Task<NotePatientDTO> AddAsync(NotePatientCreateDTO dtoc);
        Task DeleteAsync(NotePatientModel model);
        Task<APIResponse<NotePatientDTO>> GetAllAsync(NotePatientRequest request);
        Task<NotePatientModel> GetByIdAsync(int id);
        Task UpdateAsync(NotePatientUpdateDTO dtou);
    }
}