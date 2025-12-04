using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
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
        private readonly IMedicinesService _service;

        public MedicinesController(IMedicinesService service)
        {
            _service = service;
        }


        // GET: api/Medicines
        [HttpGet]
        public async Task<ActionResult<APIResponse<MedicineDTO>>> Get([FromQuery] MedicineRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Medicines/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MedicineDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            MedicineDTO dto = entity.Adapt<MedicineDTO>();
            return Ok(dto);
        }

        // POST api/Medicines
        [HttpPost]
        public async Task<ActionResult<MedicineDTO>> PostAsync([FromBody] MedicineCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Medicines/5/Activate
        [HttpPut("{id:int}/Activate")]
        public async Task<ActionResult> ActivateAsync(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var dtou = dto.Adapt<MedicineUpdateDTO>();
            dtou.IsActive = true;
            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // PUT api/Medicines/5/Disactivate
        [HttpPut("{id:int}/Disactivate")]
        public async Task<ActionResult> DisativateAsync(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var dtou = dto.Adapt<MedicineUpdateDTO>();
            dtou.IsActive = false;
            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // DELETE api/Medicines/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<MedicineModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }

    }
}