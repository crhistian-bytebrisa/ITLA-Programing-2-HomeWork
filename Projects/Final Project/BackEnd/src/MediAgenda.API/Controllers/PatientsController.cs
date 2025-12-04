using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _service;

        public PatientsController(IPatientsService service)
        {
            _service = service;
        }
        // GET: api/Patients
        [HttpGet]
        public async Task<ActionResult<APIResponse<PatientDTO>>> Get([FromQuery] PatientRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Patients/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PatientDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            PatientDTO dto = entity.Adapt<PatientDTO>();
            return Ok(dto);
        }

        // GET api/Patients/5/Notes
        [HttpGet("{id:int}/Notes")]
        public async Task<ActionResult<APIResponse<NotePatientDTO>>> GetPatientDays(int id, [FromQuery] PatientNoteRequest request)
        {
            var dto = await _service.GetAllNotes(id, request);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // GET api/Patients/5/Consultations
        [HttpGet("{id:int}/Consultations")]
        public async Task<ActionResult<APIResponse<ConsultationDTO>>> GetPatientDays(int id, [FromQuery] PatientConsultationRequest request)
        {
            var dto = await _service.GetAllConsultations(id, request);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // GET api/Patients/5/Documents
        [HttpGet("{id:int}/Documents")]
        public async Task<ActionResult<APIResponse<MedicalDocumentDTO>>> GetPatientDays(int id, [FromQuery] PatientMedicalDocumentRequest request)
        {
            var dto = await _service.GetAllMedicalDocuments(id, request);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // GET api/Patients/5/Medicines
        [HttpGet("{id:int}/Medicines")]
        public async Task<ActionResult<APIResponse<MedicineDTO>>> GetPatientDays(int id, [FromQuery] PatientMedicamentRequest request)
        {
            var dto = await _service.GetAllMedicaments(id, request);
            if (dto == null)
            {
                return NotFound();
            }
            return Ok(dto);
        }

        // POST api/Patients
        [HttpPost]
        public async Task<ActionResult<PatientDTO>> PostAsync([FromBody] PatientCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Patients/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> PutAsync(int id, [FromBody] PatientUpdateDTO dtou)
        {

            if (ModelState.ErrorCount > 0)
            {
                return ValidationProblem();
            }

            await _service.UpdateAsync(dtou);
            return NoContent();
        }

        // DELETE api/Patients/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<PatientModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }

    }
}
