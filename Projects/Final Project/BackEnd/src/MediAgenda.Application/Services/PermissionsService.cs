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
    public class PermissionsService : IPermissionsService
    {
        private readonly IPermissionRepository _repo;

        public PermissionsService(IPermissionRepository repo)
        {
            _repo = repo;
        }

        public async Task<PermissionModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<PermissionDTO>> GetAllAsync(PermissionRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<PermissionDTO> listdto = list.Adapt<List<PermissionDTO>>();

            var APIR = new APIResponse<PermissionDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<PermissionDTO> AddAsync(PermissionCreateDTO dtoc)
        {
            var model = dtoc.Adapt<PermissionModel>();
            await _repo.AddAsync(model);
            return model.Adapt<PermissionDTO>();
        }

        public async Task UpdateAsync(PermissionUpdateDTO dtou)
        {
            var model = dtou.Adapt<PermissionModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(PermissionModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
