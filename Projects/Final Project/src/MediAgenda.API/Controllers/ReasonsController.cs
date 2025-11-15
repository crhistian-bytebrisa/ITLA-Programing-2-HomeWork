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
    [Route("api/Reasons")]
    [ApiController]
    public class ReasonsController : ControllerBase
    {
        private readonly IReasonRepository _repo;

        public ReasonsController(IReasonRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Reasons
        [HttpGet]
        public async Task<ActionResult<List<ReasonDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<ReasonDTO> listdto = list.Adapt<List<ReasonDTO>>();
            return Ok(list);
        }

        // GET api/Reasons/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReasonDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ReasonDTO dto = entity.Adapt<ReasonDTO>();
            return Ok(dto);
        }

        // POST api/Reasons
        [HttpPost]
        public async Task<ActionResult<ReasonDTO>> PostAsync([FromBody] ReasonCreateDTO entity)
        {
            var model = entity.Adapt<ReasonModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<ReasonDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new {id = model.Id}, value: dto);

        }

        // PUT api/Reasons/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] ReasonCreateDTO entity)
        {
            var model = entity.Adapt<ReasonModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Reasons/5
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

        // GET: api/Reasons/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<List<ReasonDTO>>> GetByRequest([FromQuery] ReasonRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<ReasonDTO> listdto = list.Item1.Adapt<List<ReasonDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<ReasonDTO>(listdto,TotalCount,request.Page?? 1,request.PageSize?? 10);

            return Ok(APIR);
        }
    }
}
