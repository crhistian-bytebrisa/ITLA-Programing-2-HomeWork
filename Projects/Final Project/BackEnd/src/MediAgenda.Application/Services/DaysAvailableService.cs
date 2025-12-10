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
    public class DayAvailablesService : IDayAvailablesService
    {
        private readonly IDayAvailableRepository _repo;

        public DayAvailablesService(IDayAvailableRepository repo)
        {
            _repo = repo;
        }

        public async Task<DayAvailableModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<DayAvailableDTO>> GetAllAsync(DayAvailableRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<DayAvailableDTO> listdto = list.Adapt<List<DayAvailableDTO>>();

            var APIR = new APIResponse<DayAvailableDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<DayAvailableDTO> AddAsync(DayAvailableCreateDTO dtoc)
        {
            var model = dtoc.Adapt<DayAvailableModel>();
            await _repo.AddAsync(model);
            return model.Adapt<DayAvailableDTO>();
        }

        public async Task UpdateAsync(DayAvailableUpdateDTO dtou)
        {
            var model = dtou.Adapt<DayAvailableModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(DayAvailableModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
