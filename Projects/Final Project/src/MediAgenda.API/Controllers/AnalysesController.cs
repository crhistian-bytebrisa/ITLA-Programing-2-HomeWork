using Mapster;
using MediAgenda.API.DTOs;
using MediAgenda.API.DTOs.API;
using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Repositories;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Analyses")]
    [ApiController]
    public class AnalysesController : ControllerBase
    {
        private readonly IAnalysisRepository _repo;

        public AnalysesController(IAnalysisRepository repository)
        {
            _repo = repository;
        }

        // GET: api/Analyses
        [HttpGet]
        public async Task<ActionResult<List<AnalysisDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<AnalysisDTO> listdto = list.Adapt<List<AnalysisDTO>>();
            return Ok(listdto);
        }

        // GET api/Analyses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnalysisDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            AnalysisDTO dto = entity.Adapt<AnalysisDTO>();
            return Ok(dto);
        }

        // POST api/Analyses
        [HttpPost]
        public async Task<ActionResult<AnalysisDTO>> PostAsync([FromBody] AnalysisCreateDTO entity)
        {
            var model = entity.Adapt<AnalysisModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<AnalysisDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT api/Analyses/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] AnalysisCreateDTO value)
        {
            var model = value.Adapt<AnalysisModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Analyses/5
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


        // GET api/Analyses/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<List<AnalysisDTO>>> GetByRequest([FromQuery] AnalysisRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<AnalysisDTO> listdto = list.Item1.Adapt<List<AnalysisDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<AnalysisDTO>(listdto,TotalCount,request.Page?? 1, request.PageSize?? 10);

            return Ok(APIR);
        }
    }
}
