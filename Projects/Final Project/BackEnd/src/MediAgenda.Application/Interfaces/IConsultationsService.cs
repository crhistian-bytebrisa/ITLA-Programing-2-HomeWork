using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IConsultationsService
    {
        Task<ConsultationDTO> AddAsync(ConsultationCreateDTO dtoc);
        Task DeleteAsync(ConsultationModel model);
        Task<APIResponse<ConsultationDTO>> GetAllAsync(ConsultationRequest request);
        Task<ConsultationModel> GetByIdAsync(int id);
        Task UpdateAsync(ConsultationUpdateDTO dtou);
        Task<APIResponse<NoteConsultationDTO>> GetAllNotes(int id, ConsultationNoteRequest request);
    }
}