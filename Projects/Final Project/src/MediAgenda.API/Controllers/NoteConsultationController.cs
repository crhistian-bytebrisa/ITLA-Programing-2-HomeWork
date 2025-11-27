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
    [Route("api/NoteConsultations")]
    [ApiController]
    public class NoteConsultationsController : ControllerBase
    {
        private readonly INoteConsultationRepository _repo;

        public NoteConsultationsController(INoteConsultationRepository repo)
        {
            _repo = repo;
        }

        // GET: api/NoteConsultations
        [HttpGet]
        public async Task<ActionResult<List<NoteConsultationDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<NoteConsultationDTO> listdto = list.Adapt<List<NoteConsultationDTO>>();
            return Ok(list);
        }

        // GET api/NoteConsultations/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<NoteConsultationDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            NoteConsultationDTO dto = entity.Adapt<NoteConsultationDTO>();
            return Ok(dto);
        }

        // POST api/NoteConsultations
        [HttpPost]
        public async Task<ActionResult<NoteConsultationDTO>> PostAsync([FromBody] NoteConsultationCreateDTO entity)
        {
            var model = entity.Adapt<NoteConsultationModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<NoteConsultationDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/NoteConsultations/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] NoteConsultationCreateDTO entity)
        {
            var model = entity.Adapt<NoteConsultationModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/NoteConsultations/5
        [HttpDelete("{id:int}")]
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

       
    }
}
