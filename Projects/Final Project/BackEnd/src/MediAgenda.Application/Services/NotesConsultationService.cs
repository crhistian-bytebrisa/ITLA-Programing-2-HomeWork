using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class NotesConsultationService : INotesConsultationService
    {
        private readonly INoteConsultationRepository _repo;

        public NotesConsultationService(INoteConsultationRepository repo)
        {
            _repo = repo;
        }

        public async Task<NoteConsultationModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<NoteConsultationDTO>> GetAllAsync(NoteConsultationRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<NoteConsultationDTO> listdto = list.Adapt<List<NoteConsultationDTO>>();

            var APIR = new APIResponse<NoteConsultationDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<NoteConsultationDTO> AddAsync(NoteConsultationCreateDTO dtoc)
        {
            var model = dtoc.Adapt<NoteConsultationModel>();
            model.CreatedAt = DateTime.UtcNow;
            model.UpdateAt = model.CreatedAt;
            await _repo.AddAsync(model);
            return model.Adapt<NoteConsultationDTO>();
        }

        public async Task UpdateAsync(NoteConsultationUpdateDTO dtou)
        {
            var d = await _repo.GetByIdAsync(dtou.Id);

            var model = dtou.Adapt<NoteConsultationModel>();
            model.UpdateAt = DateTime.UtcNow;
            model.CreatedAt = d.CreatedAt;
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(NoteConsultationModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
