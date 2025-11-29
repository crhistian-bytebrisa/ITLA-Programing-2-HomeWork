using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/Insurances")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly IInsurancesService _service;

        public InsurancesController(IInsurancesService service)
        {
            _service = service;
        }


        // GET: api/Insurances
        [HttpGet]
        public async Task<ActionResult<APIResponse<InsuranceDTO>>> Get([FromQuery] InsuranceRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Insurances/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<InsuranceDTO>> Get(int id)
        {

            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            InsuranceDTO dto = entity.Adapt<InsuranceDTO>();
            return Ok(dto);
        }

        // POST api/Insurances
        [HttpPost]
        public async Task<ActionResult<InsuranceDTO>> PostAsync([FromBody] InsuranceCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Insurances/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] InsuranceUpdateDTO dtou)
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

        // DELETE api/Insurances/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<InsuranceModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }

    }
}
