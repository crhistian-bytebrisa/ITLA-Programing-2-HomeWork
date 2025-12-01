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
    public class MedicinesService : IMedicinesService
    {
        private readonly IMedicineRepository _repo;

        public MedicinesService(IMedicineRepository repo)
        {
            _repo = repo;
        }

        public async Task<MedicineModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<MedicineDTO>> GetAllAsync(MedicineRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<MedicineDTO> listdto = list.Adapt<List<MedicineDTO>>();

            var APIR = new APIResponse<MedicineDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<MedicineDTO> AddAsync(MedicineCreateDTO dtoc)
        {
            var model = dtoc.Adapt<MedicineModel>();
            await _repo.AddAsync(model);
            return model.Adapt<MedicineDTO>();
        }

        public async Task UpdateAsync(MedicineUpdateDTO dtou)
        {
            var model = dtou.Adapt<MedicineModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(MedicineModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
