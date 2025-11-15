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
    [Route("api/Doctors")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorRepository _repo;

        public DoctorsController(IDoctorRepository repo)
        {
            _repo = repo;
        }

        // GET: api/Doctors
        [HttpGet]
        public async Task<ActionResult<List<DoctorDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<DoctorDTO> listdto = list.Adapt<List<DoctorDTO>>();
            return Ok(list);
        }

        // GET api/Doctors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            DoctorDTO dto = entity.Adapt<DoctorDTO>();
            return Ok(dto);
        }

        // POST api/Doctors
        [HttpPost]
        public async Task<ActionResult<DoctorDTO>> PostAsync([FromBody] DoctorCreateDTO entity)
        {
            var model = entity.Adapt<DoctorModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<DoctorDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);

        }

        // PUT api/Doctors/5
        [HttpPut("{id}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] DoctorCreateDTO entity)
        {
            var model = entity.Adapt<DoctorModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/Doctors/5
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
    }
}
