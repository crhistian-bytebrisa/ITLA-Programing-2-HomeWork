using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/DaysAvailable")]
    [ApiController]
    public class DaysAvailableController : ControllerBase
    {
        private readonly IDayAvailableRepository _repo;

        public DaysAvailableController(IDayAvailableRepository repository)
        {
            _repo = repository;
        }

        // GET: api/DaysAvailable
        [HttpGet]
        public async Task<ActionResult<List<DayAvailableDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<DayAvailableDTO> listdto = list.Adapt<List<DayAvailableDTO>>();
            return Ok(listdto);
        }

        // GET: api/DaysAvailable/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DayAvailableDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            DayAvailableDTO dto = entity.Adapt<DayAvailableDTO>();
            return Ok(dto);
        }

        // POST: api/DaysAvailable
        [HttpPost]
        public async Task<ActionResult<DayAvailableDTO>> PostAsync([FromBody] DayAvailableCreateDTO entity)
        {
            var model = entity.Adapt<DayAvailableModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<DayAvailableDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT: api/DaysAvailable/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] DayAvailableCreateDTO value)
        {
            var model = value.Adapt<DayAvailableModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE: api/DaysAvailable/5
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

        // GET: api/DaysAvailable/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<APIResponse<DayAvailableDTO>>> GetByRequest([FromQuery] DayAvailableRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<DayAvailableDTO> listdto = list.Item1.Adapt<List<DayAvailableDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<DayAvailableDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}