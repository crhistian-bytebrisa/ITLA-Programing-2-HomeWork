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
    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly IApplicationUserRepository _repo;

        public ApplicationUsersService(IApplicationUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<ApplicationUserModel> GetByIdAsync(string id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<ApplicationUserDTO>> GetAllAsync(ApplicationUserRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<ApplicationUserDTO> listdto = list.Adapt<List<ApplicationUserDTO>>();

            var APIR = new APIResponse<ApplicationUserDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<ApplicationUserDTO> AddAsync(ApplicationUserCreateDTO dtoc)
        {
            var model = dtoc.Adapt<ApplicationUserModel>();
            await _repo.AddAsync(model);
            return model.Adapt<ApplicationUserDTO>();
        }

        public async Task UpdateAsync(ApplicationUserUpdateDTO dtou)
        {
            var model = dtou.Adapt<ApplicationUserModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(ApplicationUserModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
