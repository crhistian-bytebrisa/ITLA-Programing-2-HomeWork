using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Medicines")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Doctor,Admin")]
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
        [HttpPut("{id:int}")]
        public async Task<ActionResult> ActivateAsync(int id, [FromBody] MedicineUpdateDTO dtou)
        {
            if (id != dtou.Id)
            {
                ModelState.AddModelError("Id", "Deben tener el mismo Id.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }
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