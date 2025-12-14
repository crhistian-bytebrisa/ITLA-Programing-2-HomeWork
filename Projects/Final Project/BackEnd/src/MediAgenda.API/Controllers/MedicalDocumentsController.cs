using Mapster;
using MediAgenda.API.Filters;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Validations;
using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace MediAgenda.API.Controllers
{
    [Route("api/MedicalDocuments")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "User,Doctor,Admin")]
    public class MedicalDocumentsController : ControllerBase
    {
        private readonly IMedicalDocumentsService _service;
        private readonly MedicalDocumentValidation _validation;

        public MedicalDocumentsController(IMedicalDocumentsService service, IValidationService validation)
        {
            _service = service;
            _validation = new MedicalDocumentValidation(validation);
        }


        // GET: api/MedicalDocuments
        [HttpGet]
        public async Task<ActionResult<APIResponse<MedicalDocumentDTO>>> Get([FromQuery] MedicalDocumentRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/MedicalDocuments/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<MedicalDocumentDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            MedicalDocumentDTO dto = entity.Adapt<MedicalDocumentDTO>();
            return Ok(dto);
        }

        // GET api/MedicalDocuments/5/Download
        [HttpGet("{id:int}/Download")]
        public async Task<IActionResult> Download(int id)
        {
            var file = await _service.GetFileByIdAsync(id);

            return File(file.Item1, file.Item2, file.Item3);
        }

        // GET api/MedicalDocuments/5/Open
        [HttpGet("{id:int}/Open")]
        public async Task<IActionResult> Open(int id)
        {
            var file = await _service.GetFileByIdAsync(id);
            return File(file.Item1, file.Item2);
        }

        // POST api/MedicalDocuments/Upload
        [HttpPost("Upload")]
        [RequestSizeLimit(10485760)]
        public async Task<ActionResult<MedicalDocumentDTO>> PostAsync(MedicalDocumentCreateDTO dtoc)
        {
            var result = await _validation.ValidateAsync(dtoc);

            if (!result.IsValid)
            {
                result.Errors.ForEach(x => ModelState.AddModelError($"{x.PropertyName}", $"{x.ErrorMessage}"));
                return ValidationProblem();
            }

            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        

        // DELETE api/MedicalDocuments/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<MedicalDocumentModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }
    }
}