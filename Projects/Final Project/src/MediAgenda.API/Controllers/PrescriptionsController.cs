using Mapster;
using MediAgenda.API.DTOs;
using MediAgenda.API.DTOs.API;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Prescriptions")]
    [ApiController]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionRepository _repo;

        public PrescriptionsController(IPrescriptionRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<List<PrescriptionDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<PrescriptionDTO> listdto = list.Adapt<List<PrescriptionDTO>>();
            return Ok(list);
        }

        // GET api/Prescriptions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PrescriptionDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            PrescriptionDTO dto = entity.Adapt<PrescriptionDTO>();
            return Ok(dto);
        }

        // POST api/Prescriptions
        [HttpPost]
        public async Task<ActionResult<PrescriptionDTO>> PostAsync([FromBody] PrescriptionCreateDTO entity)
        {
            var model = entity.Adapt<PrescriptionModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<PrescriptionDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/Prescriptions/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] PrescriptionCreateDTO entity)
        {
            var model = entity.Adapt<PrescriptionModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Prescriptions/5
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

        // GET: api/Prescriptions/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<List<PrescriptionDTO>>> GetByRequest([FromQuery] PrescriptionRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<PrescriptionDTO> listdto = list.Item1.Adapt<List<PrescriptionDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<PrescriptionDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}