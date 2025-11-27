using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Validations;
using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Repositories;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Host;
using NuGet.Protocol;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Analyses")]
    [ApiController]
    public class AnalysesController : ControllerBase
    {
        private readonly IAnalysesService _service;

        public AnalysesController(IAnalysesService service)
        {
            _service = service;
        }

        private bool ValidateId(int id)
        {
            if(new IdIntValidation().Validate(id).IsValid)
            {
                return true;
            }
            else
            {
                ModelState.AddModelError("Id", "El Id es invalido.");
                return false;
            }
        }

        // GET: api/Analyses
        [HttpGet]
        public async Task<ActionResult<APIResponse<AnalysisDTO>>> Get([FromQuery] AnalysisRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Analyses/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<AnalysisDTO>> Get(int id)
        {
            ValidateId(id);

            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }

            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            AnalysisDTO dto = entity.Adapt<AnalysisDTO>();
            return Ok(dto);
        }

        // POST api/Analyses
        [HttpPost]
        public async Task<ActionResult<AnalysisDTO>> PostAsync([FromBody] AnalysisCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Analyses/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] AnalysisUpdateDTO dtou)
        {
            ValidateId(id);

            if (id != dtou.Id)
            {
                ModelState.AddModelError("Id", "Deben tener el mismo Id.");
            }

            if(ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }

            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // DELETE api/Analyses/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<AnalysisModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }
        
    }
}
