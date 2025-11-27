using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/MedicalDocuments")]
    [ApiController]
    public class MedicalDocumentsController : ControllerBase
    {
        private readonly IMedicalDocumentRepository _repo;

        public MedicalDocumentsController(IMedicalDocumentRepository repository)
        {
            _repo = repository;
        }

        // GET: api/MedicalDocuments
        [HttpGet]
        public async Task<ActionResult<List<MedicalDocumentDTO>>> Get()
        {
            var list = await _repo.GetAllAsync();
            List<MedicalDocumentDTO> listdto = list.Adapt<List<MedicalDocumentDTO>>();
            return Ok(listdto);
        }

        // GET api/MedicalDocuments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MedicalDocumentDTO>> Get(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound();
            }
            MedicalDocumentDTO dto = entity.Adapt<MedicalDocumentDTO>();
            return Ok(dto);
        }

        // POST api/MedicalDocuments
        [HttpPost]
        public async Task<ActionResult<MedicalDocumentDTO>> PostAsync([FromBody] MedicalDocumentCreateDTO entity)
        {
            var model = entity.Adapt<MedicalDocumentModel>();
            await _repo.AddAsync(model);
            var dto = model.Adapt<MedicalDocumentDTO>();
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = model.Id }, value: dto);
        }

        // PUT api/MedicalDocuments/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] MedicalDocumentCreateDTO value)
        {
            var model = value.Adapt<MedicalDocumentModel>();
            model.Id = id;
            await _repo.UpdateAsync(model);
            return NoContent();
        }

        // DELETE api/MedicalDocuments/5
        [HttpDelete("{id:int}")]
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


    }
}