using Mapster;
using MediAgenda.API.Filters;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MediAgenda.API.Controllers
{
    [Route("api/Patients")]
    [ApiController]
    [Authorize]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsService _service;

        public PatientsController(IPatientsService service)
        {
            _service = service;
        }
        // GET: api/Patients
        [HttpGet]
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult<APIResponse<PatientDTO>>> Get([FromQuery] PatientRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Patients/5
        [HttpGet("{id:int}")]
        [AuthorizeSamePatientIdOrRoles("id","Doctor","Admin")]
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
        [Authorize(Roles = "Doctor,Admin")]
        public async Task<ActionResult<APIResponse<NotePatientDTO>>> GetNotes(int id, [FromQuery] PatientNoteRequest request)
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
        [AuthorizeSamePatientIdOrRoles("id", "Doctor", "Admin")]
        public async Task<ActionResult<APIResponse<ConsultationDTO>>> GetConsultations(int id, [FromQuery] PatientConsultationRequest request)
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
        [AuthorizeSamePatientIdOrRoles("id", "Doctor", "Admin")]
        public async Task<ActionResult<APIResponse<MedicalDocumentDTO>>> GetDocuments(int id, [FromQuery] PatientMedicalDocumentRequest request)
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
        [AuthorizeSamePatientIdOrRoles("id", "Doctor", "Admin")]
        public async Task<ActionResult<APIResponse<MedicineDTO>>> GetMedicines(int id, [FromQuery] PatientMedicamentRequest request)
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
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<PatientDTO>> PostAsync([FromBody] PatientCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }

        // PUT api/Patients/5
        [HttpPut("{id:int}")]
        [AuthorizeSamePatientIdOrRoles("id", "Doctor", "Admin")]
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
        [Authorize(Roles = "Admin")]
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
