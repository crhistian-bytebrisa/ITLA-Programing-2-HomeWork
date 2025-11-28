using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Validations;
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
        private readonly IApplicationUsersService _service;

        public ApplicationUsersController(IApplicationUsersService service)
        {
            _service = service;
        }
        // GET: api/ApplicationUsers
        [HttpGet]
        public async Task<ActionResult<APIResponse<ApplicationUserDTO>>> Get([FromQuery] ApplicationUserRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/ApplicationUsers/klkm-anig-aaaa
        [HttpGet("{id}")]
        public async Task<ActionResult<ApplicationUserDTO>> Get(Guid id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            ApplicationUserDTO dto = entity.Adapt<ApplicationUserDTO>();
            return Ok(dto);
        }

        // POST api/ApplicationUsers
        [HttpPost]
        public async Task<ActionResult<ApplicationUserDTO>> PostAsync([FromBody] ApplicationUserCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/ApplicationUsers/klkm-anig-aaaa
        [HttpPut("{ids}")]
        public async Task<ActionResult> PutAsync(Guid id, [FromBody] ApplicationUserUpdateDTO dtou)
        {
            if(id != dtou.Id)
            {
                ModelState.AddModelError("Id", "El Id en la URL no coincide con el Id en el cuerpo de la solicitud.");
            }

            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }
            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // DELETE api/ApplicationUsers/klkm-anig-aaaa
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<ApplicationUserModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }
    }
}
