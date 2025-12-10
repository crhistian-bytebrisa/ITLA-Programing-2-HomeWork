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
    public class DoctorsService : IDoctorsService
    {
        private readonly IDoctorRepository _repo;

        public DoctorsService(IDoctorRepository repo)
        {
            _repo = repo;
        }

        public async Task<DoctorModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<DoctorDTO>> GetAllAsync(DoctorRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<DoctorDTO> listdto = list.Adapt<List<DoctorDTO>>();

            var APIR = new APIResponse<DoctorDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<DoctorDTO> AddAsync(DoctorCreateDTO dtoc)
        {
            var model = dtoc.Adapt<DoctorModel>();
            await _repo.AddAsync(model);
            return model.Adapt<DoctorDTO>();
        }

        public async Task UpdateAsync(DoctorUpdateDTO dtou)
        {
            var model = dtou.Adapt<DoctorModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(DoctorModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
