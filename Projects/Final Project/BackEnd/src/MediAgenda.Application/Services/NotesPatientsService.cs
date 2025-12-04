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
    public class NotesPatientsService : INotesPatientsService
    {
        private readonly INotePatientRepository _repo;

        public NotesPatientsService(INotePatientRepository repo)
        {
            _repo = repo;
        }

        public async Task<NotePatientModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<NotePatientDTO>> GetAllAsync(NotePatientRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<NotePatientDTO> listdto = list.Adapt<List<NotePatientDTO>>();

            var APIR = new APIResponse<NotePatientDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<NotePatientDTO> AddAsync(NotePatientCreateDTO dtoc)
        {
            var model = dtoc.Adapt<NotePatientModel>();
            model.CreatedAt = DateTime.UtcNow;
            model.UpdateAt = model.CreatedAt;
            await _repo.AddAsync(model);
            return model.Adapt<NotePatientDTO>();
        }

        public async Task UpdateAsync(NotePatientUpdateDTO dtou)
        {
            var d = await _repo.GetByIdAsync(dtou.Id);

            var model = dtou.Adapt<NotePatientModel>();
            model.UpdateAt = DateTime.UtcNow;
            model.CreatedAt = d.CreatedAt;
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(NotePatientModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
