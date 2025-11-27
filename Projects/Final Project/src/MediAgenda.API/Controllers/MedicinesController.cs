using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Medicines")]
    [ApiController]
    public class MedicinesController : ControllerBase
    {
        private readonly IMedicineRepository _repo;

        public MedicinesController(IMedicineRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<List<MedicineDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<MedicineDTO> listdto = list.Adapt<List<MedicineDTO>>();
            return Ok(list);
        }

        // GET api/Medicines/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MedicineDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            MedicineDTO dto = entity.Adapt<MedicineDTO>();
            return Ok(dto);
        }

        // POST api/Medicines
        [HttpPost]
        public async Task<ActionResult<MedicineDTO>> PostAsync([FromBody] MedicineCreateDTO entity)
        {
            var model = entity.Adapt<MedicineModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<MedicineDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/Medicines/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] MedicineCreateDTO entity)
        {
            var model = entity.Adapt<MedicineModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Medicines/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);

            if (entity is null)
            {
                return NotFound();
            }

            await _repo.DeleteAsync(entity);
            return NoContent();
        }

        
    }
}