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
    [Route("api/Consultations")]
    [ApiController]
    public class ConsultationsController : ControllerBase
    {
        private readonly IConsultationRepository _repo;

        public ConsultationsController(IConsultationRepository repository)
        {
            _repo = repository;
        }

        // GET: api/Consultations
        [HttpGet]
        public async Task<ActionResult<List<ConsultationDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<ConsultationDTO> listdto = list.Adapt<List<ConsultationDTO>>();
            return Ok(listdto);
        }

        // GET: api/Consultations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ConsultationDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ConsultationDTO dto = entity.Adapt<ConsultationDTO>();
            return Ok(dto);
        }

        // POST: api/Consultations
        [HttpPost]
        public async Task<ActionResult<ConsultationDTO>> PostAsync([FromBody] ConsultationCreateDTO entity)
        {
            var model = entity.Adapt<ConsultationModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<ConsultationDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT: api/Consultations/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ConsultationCreateDTO value)
        {
            var model = value.Adapt<ConsultationModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE: api/Consultations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            await _repo.DeleteAsync(entity);
            return NoContent();
        }

        // GET: api/Consultations/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<APIResponse<ConsultationDTO>>> GetByRequest([FromQuery] ConsultationRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<ConsultationDTO> listdto = list.Item1.Adapt<List<ConsultationDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<ConsultationDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}