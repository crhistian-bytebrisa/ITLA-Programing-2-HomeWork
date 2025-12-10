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
    public class InsurancesService : IInsurancesService
    {
        private readonly IInsuranceRepository _repo;

        public InsurancesService(IInsuranceRepository repo)
        {
            _repo = repo;
        }

        public async Task<InsuranceModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<InsuranceDTO>> GetAllAsync(InsuranceRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<InsuranceDTO> listdto = list.Adapt<List<InsuranceDTO>>();

            var APIR = new APIResponse<InsuranceDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<InsuranceDTO> AddAsync(InsuranceCreateDTO dtoc)
        {
            var model = dtoc.Adapt<InsuranceModel>();
            await _repo.AddAsync(model);
            return model.Adapt<InsuranceDTO>();
        }

        public async Task UpdateAsync(InsuranceUpdateDTO dtou)
        {
            var model = dtou.Adapt<InsuranceModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(InsuranceModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
