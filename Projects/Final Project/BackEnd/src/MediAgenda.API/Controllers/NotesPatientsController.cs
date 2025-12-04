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
    [Route("api/NotesPatients")]
    [ApiController]
    public class NotesPatientsController : ControllerBase
    {
        private readonly INotesPatientsService _service;

        public NotesPatientsController(INotesPatientsService service)
        {
            _service = service;
        }

        // GET: api/NotesPatients
        [HttpGet]
        public async Task<ActionResult<APIResponse<NotePatientDTO>>> Get([FromQuery] NotePatientRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/NotesPatients/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NotePatientDTO>> Get(int id)
        {

            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            NotePatientDTO dto = entity.Adapt<NotePatientDTO>();
            return Ok(dto);
        }

        // POST api/NotesPatients
        [HttpPost]
        public async Task<ActionResult<NotePatientDTO>> PostAsync([FromBody] NotePatientCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/NotesPatients/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] NotePatientUpdateDTO dtou)
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

        // DELETE api/NotesPatients/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<NotePatientModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }
    }
}