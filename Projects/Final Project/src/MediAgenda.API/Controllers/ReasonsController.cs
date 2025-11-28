using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Validations;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Reasons")]
    [ApiController]
    public class ReasonsController : ControllerBase
    {
        private readonly IReasonsService _service;

        public ReasonsController(IReasonsService service)
        {
            _service = service;
        }

       
        // GET: api/Reasons
        [HttpGet]
        public async Task<ActionResult<APIResponse<ReasonDTO>>> Get([FromQuery] ReasonRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Reasons/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReasonDTO>> Get(int id)
        {

            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            ReasonDTO dto = entity.Adapt<ReasonDTO>();
            return Ok(dto);
        }

        // POST api/Reasons
        [HttpPost]
        public async Task<ActionResult<ReasonDTO>> PostAsync([FromBody] ReasonCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Reasons/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ReasonUpdateDTO dtou)
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

        // DELETE api/Reasons/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<ReasonModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }
    }
}
