using Mapster;
using MediAgenda.API.DTOs;
using MediAgenda.API.DTOs.API;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientRepository _repo;

        public PatientsController(IPatientRepository repository)
        {
            _repo = repository;
        }

        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<List<PatientDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<PatientDTO> listdto = list.Adapt<List<PatientDTO>>();
            return Ok(listdto);
        }

        // GET api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            PatientDTO dto = entity.Adapt<PatientDTO>();
            return Ok(dto);
        }

        // POST api/Patients
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> PostAsync([FromBody] PatientCreateDTO entity)
        {
            var model = entity.Adapt<PatientModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<PatientDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT api/Patients/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] PatientCreateDTO value)
        {
            var model = value.Adapt<PatientModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Patients/5
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

        // GET: api/Patients/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<APIResponse<PatientDTO>>> GetByRequest([FromQuery] PatientRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<PatientDTO> listdto = list.Item1.Adapt<List<PatientDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<PatientDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}
