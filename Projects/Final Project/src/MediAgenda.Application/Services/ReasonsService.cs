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
    public class ReasonsService : IReasonsService
    {
        private readonly IReasonRepository _repo;

        public ReasonsService(IReasonRepository repo)
        {
            _repo = repo;
        }

        public async Task<ReasonModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<ReasonDTO>> GetAllAsync(ReasonRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<ReasonDTO> listdto = list.Adapt<List<ReasonDTO>>();

            var APIR = new APIResponse<ReasonDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<ReasonDTO> AddAsync(ReasonCreateDTO dtoc)
        {
            var model = dtoc.Adapt<ReasonModel>();
            await _repo.AddAsync(model);
            return model.Adapt<ReasonDTO>();
        }

        public async Task UpdateAsync(ReasonUpdateDTO dtou)
        {
            var model = dtou.Adapt<ReasonModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(ReasonModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
