using FluentValidation;
using FluentValidation.Results;
using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Validations.UpdateValidations;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class ReasonsService : IReasonsService
    {
        private readonly IReasonRepository _repo;

        private readonly IValidator<ReasonPatchDTO> _validator;

        public ReasonsService(IReasonRepository repo, IValidator<ReasonPatchDTO> validator)
        {
            _repo = repo;
            _validator = validator;
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

        // Recibe el modelo actual y el JsonPatchDocument para aplicar los cambios
        public async Task<FluentValidation.Results.ValidationResult> PatchAsync(ReasonModel model, JsonPatchDocument<ReasonPatchDTO> dtop)
        {
            // Convertir el modelo actual a DTO
            var dto = model.Adapt<ReasonPatchDTO>();

            // Aplicar el JsonPatchDocument al DTO
            dtop.ApplyTo(dto);

            // Validar el DTO modificado
            var result = await _validator.ValidateAsync(dto);

            // Si la validación falla, devolver los errores
            if (!result.IsValid)
            {
                return result;
            }

            // Asegurar que no haya cambios en el ID
            dto.Id = model.Id;

            // Mapear los cambios del DTO de vuelta al modelo
            dto.Adapt(model);

            // Guardar los cambios en el repositorio
            await _repo.UpdateAsync(model);
            return result;
        }

        public async Task DeleteAsync(ReasonModel model)
        {
            await _repo.DeleteAsync(model);
        }
    }
}
