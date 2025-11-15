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
    [Route("api/NotePatients")]
    [ApiController]
    public class NotePatientsController : ControllerBase
    {
        private readonly INotePatientRepository _repo;

        public NotePatientsController(INotePatientRepository repo)
        {
            _repo = repo;
        }

        // GET: api/NotePatients
        [HttpGet]
        public async Task<ActionResult<List<NotePatientDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<NotePatientDTO> listdto = list.Adapt<List<NotePatientDTO>>();
            return Ok(list);
        }

        // GET api/NotePatients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotePatientDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            NotePatientDTO dto = entity.Adapt<NotePatientDTO>();
            return Ok(dto);
        }

        // POST api/NotePatients
        [HttpPost]
        public async Task<ActionResult<NotePatientDTO>> PostAsync([FromBody] NotePatientCreateDTO entity)
        {
            var model = entity.Adapt<NotePatientModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<NotePatientDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/NotePatients/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] NotePatientCreateDTO entity)
        {
            var model = entity.Adapt<NotePatientModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/NotePatients/5
        [HttpDelete("{id}")]
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

        // GET: api/NotePatients/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<List<NotePatientDTO>>> GetByRequest([FromQuery] NotePatientRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<NotePatientDTO> listdto = list.Item1.Adapt<List<NotePatientDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<NotePatientDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}