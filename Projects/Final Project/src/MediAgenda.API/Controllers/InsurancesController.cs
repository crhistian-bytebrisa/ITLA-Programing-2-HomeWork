using Mapster;
using MediAgenda.API.DTOs;
using MediAgenda.API.DTOs.API;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/Insurances")]
    [ApiController]
    public class InsurancesController : ControllerBase
    {
        private readonly IInsuranceRepository _repo;

        public InsurancesController(IInsuranceRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Insurances
        [HttpGet]
        public async Task<ActionResult<List<InsuranceDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<InsuranceDTO> listdto = list.Adapt<List<InsuranceDTO>>();
            return Ok(list);
        }

        // GET api/Insurances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InsuranceDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            InsuranceDTO dto = entity.Adapt<InsuranceDTO>();
            return Ok(dto);
        }

        // POST api/Insurances
        [HttpPost]
        public async Task<ActionResult<InsuranceDTO>> PostAsync([FromBody] InsuranceCreateDTO entity)
        {
            var model = entity.Adapt<InsuranceModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<InsuranceDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/Insurances/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] InsuranceCreateDTO entity)
        {
            var model = entity.Adapt<InsuranceModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Insurances/5
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

        // GET: api/Insurances/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<List<InsuranceDTO>>> GetByRequest([FromQuery] InsuranceRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<InsuranceDTO> listdto = list.Item1.Adapt<List<InsuranceDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<InsuranceDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}
