using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Validations;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/DaysAvailable")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Doctor,Admin,User")]
    public class DaysAvailableController : ControllerBase
    {
        private readonly IDayAvailablesService _service;

        public DaysAvailableController(IDayAvailablesService service)
        {
            _service = service;
        }

        // GET: api/DaysAvailable
        [HttpGet]
        public async Task<ActionResult<APIResponse<DayAvailableDTO>>> Get([FromQuery] DayAvailableRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/DaysAvailable/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<DayAvailableDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            DayAvailableDTO dto = entity.Adapt<DayAvailableDTO>();
            return Ok(dto);
        }

        // POST api/DaysAvailable
        [HttpPost]
        public async Task<ActionResult<DayAvailableDTO>> PostAsync([FromBody] DayAvailableCreateDTO dtoc)
        {
            if (dtoc.ClinicId <= 0)
            {
                ModelState.AddModelError("ClinicId", "El ClinicId es invalido.");
                return ValidationProblem();
            }

            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/DaysAvailable/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] DayAvailableUpdateDTO dtou)
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

        // DELETE api/DaysAvailable/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<DayAvailableModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }

    }
}