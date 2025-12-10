using Mapster;
using MediAgenda.API.Filters;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Consultations")]
    [ApiController]
    [Authorize]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationsService _service;

        public ConsultationsController(IConsultationsService service)
        {
            _service = service;
        }
        // GET: api/Consultations
        [HttpGet]
        [AuthorizeSamePatientIdOrRoles("PatientId", "Doctor", "Admin")]
        public async Task<ActionResult<APIResponse<ConsultationDTO>>> Get([FromQuery] ConsultationRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Consultations/5
        [HttpGet("{id:int}")]
        [AuthorizeSamePatientIdOrRoles("PatientId", "Doctor", "Admin")]
        public async Task<ActionResult<ConsultationDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            ConsultationDTO dto = entity.Adapt<ConsultationDTO>();
            return Ok(dto);
        }

        // GET api/Consultations/5/Notes
        [HttpGet("{id:int}/Notes")]
        [Authorize(Roles = "Admin,Doctor")]
        public async Task<ActionResult<APIResponse<NoteConsultationDTO>>> GetNotes(int id, [FromQuery] ConsultationNoteRequest request)
        {
            var dto = await _service.GetAllNotes(id, request);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/Consultations
        [HttpPost]
        [AuthorizeSamePatientIdOrRoles("PatientId","Doctor","Admin")]
        public async Task<ActionResult<ConsultationDTO>> PostAsync([FromBody] ConsultationCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Consultations/5
        [HttpPut("{id:int}")]
        [AuthorizeSamePatientIdOrRoles("PatientId", "Doctor", "Admin")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ConsultationUpdateDTO dtou)
        {
            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }

            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // DELETE api/Consultations/5
        [HttpDelete("{id:int}")]
        [AuthorizeSamePatientIdOrRoles("PatientId", "Doctor", "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<ConsultationModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }


    }
}