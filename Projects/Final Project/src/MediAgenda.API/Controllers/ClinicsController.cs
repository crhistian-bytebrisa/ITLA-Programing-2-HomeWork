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
    [Route("api/Clinics")]
    [ApiController]
    public class ClinicsController : ControllerBase
    {
        private readonly IClinicRepository _repo;

        public ClinicsController(IClinicRepository repository)
        {
            _repo = repository;
        }

        // GET: api/Clinics
        [HttpGet]
        public async Task<ActionResult<List<ClinicDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<ClinicDTO> listdto = list.Adapt<List<ClinicDTO>>();
            return Ok(listdto);
        }

        // GET: api/Clinics/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClinicDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ClinicDTO dto = entity.Adapt<ClinicDTO>();
            return Ok(dto);
        }

        // POST: api/Clinics
        [HttpPost]
        public async Task<ActionResult<ClinicDTO>> PostAsync([FromBody] ClinicCreateDTO entity)
        {
            var model = entity.Adapt<ClinicModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<ClinicDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT: api/Clinics/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ClinicCreateDTO value)
        {
            var model = value.Adapt<ClinicModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE: api/Clinics/5
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

        // GET: api/Clinics/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<APIResponse<ClinicDTO>>> GetByRequest([FromQuery] ClinicRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<ClinicDTO> listdto = list.Item1.Adapt<List<ClinicDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<ClinicDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}
