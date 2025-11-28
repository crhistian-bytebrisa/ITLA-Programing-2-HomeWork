using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Services;
using MediAgenda.Application.Validations;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Clinics")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicsService _service;

        public ClinicsController(IClinicsService service)
        {
            _service = service;
        }
        // GET: api/Clinics
        [HttpGet]
        public async Task<ActionResult<APIResponse<ClinicDTO>>> Get([FromQuery] ClinicRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Clinics/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ClinicDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            ClinicDTO dto = entity.Adapt<ClinicDTO>();
            return Ok(dto);
        }

        // GET api/Clinics/5/Days
        [HttpGet("{id:int}/Days")]
        public async Task<ActionResult<List<DayAvailableDTO>>> GetClinicDays(int id,[FromQuery] ClinicDaysAvailableRequest request)
        {
            var dto = await _service.GetAllDaysAvailableById(id, request);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }


        // POST api/Clinics
        [HttpPost]
        public async Task<ActionResult<ClinicDTO>> PostAsync([FromBody] ClinicCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Clinics/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ClinicUpdateDTO dtou)
        {

            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }

            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // DELETE api/Clinics/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<ClinicModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }

    }
}
