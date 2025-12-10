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
    public class ConsultationsService : IConsultationsService
    {
        private readonly IConsultationRepository _repo;

        public ConsultationsService(IConsultationRepository repo)
        {
            _repo = repo;
        }

        public async Task<ConsultationModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<ConsultationDTO>> GetAllAsync(ConsultationRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<ConsultationDTO> listdto = list.Adapt<List<ConsultationDTO>>();

            var APIR = new APIResponse<ConsultationDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<ConsultationDTO> AddAsync(ConsultationCreateDTO dtoc)
        {
            var model = dtoc.Adapt<ConsultationModel>();
            await _repo.AddAsync(model);
            return model.Adapt<ConsultationDTO>();
        }

        public async Task UpdateAsync(ConsultationUpdateDTO dtou)
        {
            var model = dtou.Adapt<ConsultationModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(ConsultationModel model)
        {
            await _repo.DeleteAsync(model);
        }

        public async Task<APIResponse<NoteConsultationDTO>> GetAllNotes(int id, ConsultationNoteRequest request)
        {
            var (list, count) = await _repo.GetAllNotesById(id, request);

            List<NoteConsultationDTO> listdto = list.Adapt<List<NoteConsultationDTO>>();

            var APIR = new APIResponse<NoteConsultationDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }
    }
}
