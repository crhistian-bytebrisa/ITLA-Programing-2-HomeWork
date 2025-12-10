using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Application.Interfaces;
using MediAgenda.Application.Services;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Threading.Tasks;

namespace MediAgenda.API.Controllers
{
    [Route("api/Prescriptions")]
    [ApiController]
    [Authorize]
    [Authorize(Roles = "Admin,Doctor")]
    public class PrescriptionsController : ControllerBase
    {
        private readonly IPrescriptionsService _service;

        public PrescriptionsController(IPrescriptionsService service)
        {
            _service = service;
        }

        // GET: api/Prescriptions
        [HttpGet]
        public async Task<ActionResult<APIResponse<PrescriptionDTO>>> Get([FromQuery] PrescriptionRequest request)
        {
            var APIR = await _service.GetAllAsync(request);
            return Ok(APIR);
        }

        // GET api/Prescriptions/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PrescriptionDTO>> Get(int id)
        {
            var entity = await _service.GetByIdAsync(id);

            if (entity == null)
            {
                return NotFound();
            }

            PrescriptionDTO dto = entity.Adapt<PrescriptionDTO>();
            return Ok(dto);
        }

        // POST api/Prescriptions
        [HttpPost]
        public async Task<ActionResult<PrescriptionDTO>> PostAsync([FromBody] PrescriptionCreateDTO dtoc)
        {
            var dto = await _service.AddAsync(dtoc);
            return CreatedAtAction(actionName: nameof(Get), routeValues: new { id = dto.Id }, value: dto);
        }
        
        // DELETE api/Prescriptions/5
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var dto = await _service.GetByIdAsync(id);
            if (dto == null)
            {
                return NotFound();
            }
            var model = dto.Adapt<PrescriptionModel>();
            await _service.DeleteAsync(model);
            return NoContent();
        }

        #region Medicinas

        [HttpGet("{id:int}/Medicines")]
        //GET api/Prescriptions/5/Medicines
        public async Task<ActionResult<List<MedicineDTO>>> GetMedicineInPrescription(int id)
        {
            return await _service.GetAllMedicine(id);
        }


        // POST api/Prescriptions/5/Medicines
        [HttpPost("{id:int}/Medicines")]
        public async Task<ActionResult<PrescriptionMedicineDTO>> AddMedicineInPrescription(int id, PrescriptionMedicineCUDTO dtocu)
        {
            if (dtocu.PrescriptionId != id)
            {
                return BadRequest("Deberias de poner el id igual.");
            }

            var entity = await _service.AddMedicineAsync(dtocu);

            return Ok(entity);
        }

        // PUT api/Prescription/5/Medicines/1
        [HttpPut("{id:int}/Medicines/{idmedi:int}")]
        public async Task<ActionResult<PrescriptionMedicineDTO>> UpdateMedicineInPrescription(int id, int idmedi, PrescriptionMedicineCUDTO dtocu)
        {
            if (dtocu.PrescriptionId != id || dtocu.MedicineId == idmedi)
            {
                return BadRequest("Deberias de poner el id igual.");
            }

            var entity = await _service.UpdateMedicineAsync(dtocu);

            return Ok(entity);
        }

        // DELETE api/Prescription/5/Medicines/1
        [HttpDelete("{id:int}/Medicines/{idmedi:int}")]
        public async Task<ActionResult<PrescriptionMedicineDTO>> DeleteMedicineInPrescription(int id, int idmedi)
        {
            await _service.DeleteMedicineAsync(idmedi, id);

            return NoContent();
        }

        #endregion


        #region Analisis

        [HttpGet("{id:int}/Analysis")]
        //GET api/Prescriptions/5/Analysis
        public async Task<ActionResult<List<AnalysisDTO>>> GetAnalysisInPrescription(int id)
        {
            return await _service.GetAllAnalysis(id);
        }

        // POST api/Prescriptions/5/Analysis
        [HttpPost("{id:int}/Analysis")]
        public async Task<ActionResult<PrescriptionAnalysisDTO>> AddAnalysisInPrescription(int id, [FromBody] PrescriptionAnalysisCUDTO dtocu)
        {
            if (dtocu.PrescriptionId != id)
            {
                return BadRequest("Deberias de poner el id igual.");
            }

            var entity = await _service.AddAnalysisAsync(dtocu);
            return Ok(entity);
        }

        // PUT api/Prescriptions/5/Analysis/1
        [HttpPut("{id:int}/Analysis/{idanalysis:int}")]
        public async Task<ActionResult<PrescriptionAnalysisDTO>> UpdateAnalysisInPrescription(int id, int idanalysis, [FromBody] PrescriptionAnalysisCUDTO dtocu)
        {
            if (dtocu.PrescriptionId != id || dtocu.AnalysisId != idanalysis)
            {
                return BadRequest("Deberias de poner el id igual.");
            }

            var entity = await _service.UpdateAnalysisAsync(dtocu);
            return Ok(entity);
        }

        // DELETE api/Prescriptions/5/Analysis/1
        [HttpDelete("{id:int}/Analysis/{idanalysis:int}")]
        public async Task<ActionResult> DeleteAnalysisInPrescription(int id, int idanalysis)
        {
            await _service.DeleteAnalysisAsync(idanalysis, id);
            return NoContent();
        }


        #endregion

        #region Permisos

        [HttpGet("{id:int}/Permissions")]
        //GET api/Prescriptions/5/Permissions
        public async Task<ActionResult<List<PermissionDTO>>> GetPermissionsInPrescription(int id)
        {
            return await _service.GetAllPermission(id);
        }

        // POST api/Prescriptions/5/Permissions
        [HttpPost("{id:int}/Permissions")]
        public async Task<ActionResult<PrescriptionPermissionDTO>> AddPermissionInPrescription(int id, [FromBody] PrescriptionPermissionCUDTO dtocu)
        {
            if (dtocu.PrescriptionId != id)
            {
                return BadRequest("Deberias de poner el id igual.");
            }

            var entity = await _service.AddPermissionAsync(dtocu);
            return Ok(entity);
        }

        // PUT api/Prescriptions/5/Permissions/1
        [HttpPut("{id:int}/Permissions/{idpermission:int}")]
        public async Task<ActionResult<PrescriptionPermissionDTO>> UpdatePermissionInPrescription(int id, int idpermission, [FromBody] PrescriptionPermissionCUDTO dtocu)
        {
            if (dtocu.PrescriptionId != id || dtocu.PermissionId != idpermission)
            {
                return BadRequest("Deberias de poner el id igual.");
            }

            var entity = await _service.UpdatePermissionAsync(dtocu);
            return Ok(entity);
        }

        // DELETE api/Prescriptions/5/Permissions/1
        [HttpDelete("{id:int}/Permissions/{idpermission:int}")]
        public async Task<ActionResult> DeletePermissionInPrescription(int id, int idpermission)
        {
            await _service.DeletePermissionAsync(idpermission, id);
            return NoContent();
        }
        #endregion
    }
}