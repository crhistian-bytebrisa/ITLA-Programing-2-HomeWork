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
    public class AnalysesService : IAnalysesService
    {
        private readonly IAnalysisRepository _repo;

        public AnalysesService(IAnalysisRepository repo)
        {
            _repo = repo;
        }

        public async Task<AnalysisModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<AnalysisDTO>> GetAllAsync(AnalysisRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<AnalysisDTO> listdto = list.Adapt<List<AnalysisDTO>>();

            var APIR = new APIResponse<AnalysisDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<AnalysisDTO> AddAsync(AnalysisCreateDTO dtoc)
        {
            var model = dtoc.Adapt<AnalysisModel>();
            await _repo.AddAsync(model);
            return model.Adapt<AnalysisDTO>();
        }

        public async Task UpdateAsync(AnalysisUpdateDTO dtou)
        {
            var model = dtou.Adapt<AnalysisModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(AnalysisModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
