using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class ClinicsService : IClinicsService
    {
        private readonly IClinicRepository _repo;

        public ClinicsService(IClinicRepository repo)
        {
            _repo = repo;
        }

        public async Task<ClinicModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<ClinicDTO>> GetAllAsync(ClinicRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<ClinicDTO> listdto = list.Adapt<List<ClinicDTO>>();

            var APIR = new APIResponse<ClinicDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<ClinicDTO> AddAsync(ClinicCreateDTO dtoc)
        {
            var model = dtoc.Adapt<ClinicModel>();
            await _repo.AddAsync(model);
            return model.Adapt<ClinicDTO>();
        }

        public async Task UpdateAsync(ClinicUpdateDTO dtou)
        {
            var model = dtou.Adapt<ClinicModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(ClinicModel model)
        {
            await _repo.DeleteAsync(model);
        }

        public async Task<APIResponse<DayAvailableSimpleDTO>> GetAllDaysAvailableById(int id, ClinicDaysAvailableRequest request)
        {
            var (list,count) = await _repo.GetAllDaysAvailableById(id, request);
            List<DayAvailableSimpleDTO> listdto = list.Adapt<List<DayAvailableSimpleDTO>>();

            var APIR = new APIResponse<DayAvailableSimpleDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }
    }
}
