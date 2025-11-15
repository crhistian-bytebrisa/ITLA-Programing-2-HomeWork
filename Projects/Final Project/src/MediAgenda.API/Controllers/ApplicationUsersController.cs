using Mapster;
using MediAgenda.API.DTOs;
using MediAgenda.API.DTOs.API;
using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/ApplicationUsers")]
    [ApiController]
    public class ApplicationUsersController : ControllerBase
    {
        private readonly IApplicationUserRepository _repo;

        public ApplicationUsersController(IApplicationUserRepository repository)
        {
            _repo = repository;
        }

        // GET: api/ApplicationUsers
        [HttpGet]
        public async Task<ActionResult<List<ApplicationUserDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<ApplicationUserDTO> listdto = list.Adapt<List<ApplicationUserDTO>>();
            return Ok(listdto);
        }

        // GET api/ApplicationUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            ApplicationUserDTO dto = entity.Adapt<ApplicationUserDTO>();
            return Ok(dto);
        }

        // POST api/ApplicationUsers
        [HttpPost]
        public async Task<ActionResult<ApplicationUserDTO>> PostAsync([FromBody] ApplicationUserCreateDTO entity)
        {
            var model = entity.Adapt<ApplicationUserModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<ApplicationUserDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT api/ApplicationUsers/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(string id, [FromBody] ApplicationUserCreateDTO value)
        {
            var model = value.Adapt<ApplicationUserModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/ApplicationUsers/5
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


        // GET api/ApplicationUsers/ByRequest
        [HttpGet("ByRequest")]
        public async Task<ActionResult<List<ApplicationUserDTO>>> GetByRequest([FromQuery] ApplicationUserRequest request)
        {
            var list = await _repo.GetByRequest(request);
            List<ApplicationUserDTO> listdto = list.Item1.Adapt<List<ApplicationUserDTO>>();
            int TotalCount = list.Item2;

            var APIR = new APIResponse<ApplicationUserDTO>(listdto, TotalCount, request.Page ?? 1, request.PageSize ?? 10);

            return Ok(APIR);
        }
    }
}
