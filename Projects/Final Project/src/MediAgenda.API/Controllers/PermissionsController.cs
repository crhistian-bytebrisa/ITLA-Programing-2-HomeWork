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
    [Route("api/Permissions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository _repo;

        public PermissionsController(IPermissionRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult<List<PermissionDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<PermissionDTO> listdto = list.Adapt<List<PermissionDTO>>();
            return Ok(list);
        }

        // GET api/Permissions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PermissionDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            PermissionDTO dto = entity.Adapt<PermissionDTO>();
            return Ok(dto);
        }

        // POST api/Permissions
        [HttpPost]
        public async Task<ActionResult<PermissionDTO>> PostAsync([FromBody] PermissionCreateDTO entity)
        {
            var model = entity.Adapt<PermissionModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<PermissionDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/Permissions/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] PermissionCreateDTO entity)
        {
            var model = entity.Adapt<PermissionModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Permissions/5
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