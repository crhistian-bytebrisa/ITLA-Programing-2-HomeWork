using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface INotesConsultationService
    {
        Task<NoteConsultationDTO> AddAsync(NoteConsultationCreateDTO dtoc);
        Task DeleteAsync(NoteConsultationModel model);
        Task<APIResponse<NoteConsultationDTO>> GetAllAsync(NoteConsultationRequest request);
        Task<NoteConsultationModel> GetByIdAsync(int id);
        Task UpdateAsync(NoteConsultationUpdateDTO dtou);
    }
}