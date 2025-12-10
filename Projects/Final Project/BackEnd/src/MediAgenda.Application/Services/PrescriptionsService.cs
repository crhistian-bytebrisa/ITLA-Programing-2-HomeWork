using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Relations;
using MediAgenda.Infraestructure.Repositories;
using MediAgenda.Infraestructure.RequestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class PrescriptionsService : IPrescriptionsService
    {
        private readonly IPrescriptionRepository _repo;

        public PrescriptionsService(IPrescriptionRepository repo)
        {
            _repo = repo;
        }

        public async Task<PrescriptionModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<PrescriptionDTO>> GetAllAsync(PrescriptionRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<PrescriptionDTO> listdto = list.Adapt<List<PrescriptionDTO>>();

            var APIR = new APIResponse<PrescriptionDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<PrescriptionDTO> AddAsync(PrescriptionCreateDTO dtoc)
        {
            var model = dtoc.Adapt<PrescriptionModel>();
            await _repo.AddAsync(model);
            return model.Adapt<PrescriptionDTO>();
        }

        public async Task DeleteAsync(PrescriptionModel model)
        {
            await _repo.DeleteAsync(model);
        }

        public async Task<List<MedicineDTO>> GetAllMedicine(int prescriptionid)
        {
            var list = await _repo.GetMedicineInPrescription(prescriptionid);
            return list.Adapt<List<MedicineDTO>>();
        }

        public async Task<PrescriptionMedicineDTO> AddMedicineAsync(PrescriptionMedicineCUDTO dtocu)
        {
            var model = dtocu.Adapt<PrescriptionMedicineModel>();

            var medicinemodel = await _repo.AddMedicineInPrescription(model);

            return medicinemodel.Adapt<PrescriptionMedicineDTO>();
        }

        public async Task<PrescriptionMedicineDTO> UpdateMedicineAsync(PrescriptionMedicineCUDTO dtocu)
        {
            var model = dtocu.Adapt<PrescriptionMedicineModel>();

            var medicinemodel = _repo.UpdateMedicineInPrescription(model);

            return medicinemodel.Adapt<PrescriptionMedicineDTO>();
        }

        public async Task DeleteMedicineAsync(int medicineid, int prescriptionid)
        {
            await _repo.DeleteMedicineInPrescription(medicineid, prescriptionid);
        }


        public async Task<List<AnalysisDTO>> GetAllAnalysis(int prescriptionid)
        {
            var list = await _repo.GetAnalysisInPrescription(prescriptionid);
            return list.Adapt<List<AnalysisDTO>>();
        }


        public async Task<PrescriptionAnalysisDTO> AddAnalysisAsync(PrescriptionAnalysisCUDTO dtocu)
        {
            var model = dtocu.Adapt<PrescriptionAnalysisModel>();

            var analysismodel = await _repo.AddAnalysisInPrescription(model);

            return analysismodel.Adapt<PrescriptionAnalysisDTO>();
        }

        public async Task<PrescriptionAnalysisDTO> UpdateAnalysisAsync(PrescriptionAnalysisCUDTO dtocu)
        {
            var model = dtocu.Adapt<PrescriptionAnalysisModel>();

            var analysismodel = _repo.UpdateAnalysisInPrescription(model);

            return analysismodel.Adapt<PrescriptionAnalysisDTO>();
        }

        public async Task DeleteAnalysisAsync(int analysisid, int prescriptionid)
        {
            await _repo.DeleteAnalysisInPrescription(analysisid, prescriptionid);
        }

        public async Task<List<PermissionDTO>> GetAllPermission(int prescriptionid)
        {
            var list = await _repo.GetPermissionInPrescription(prescriptionid);
            return list.Adapt<List<PermissionDTO>>();
        }


        public async Task<PrescriptionPermissionDTO> AddPermissionAsync(PrescriptionPermissionCUDTO dtocu)
        {
            var model = dtocu.Adapt<PrescriptionPermissionModel>();

            var permissionmodel = await _repo.AddPermissionInPrescription(model);

            return permissionmodel.Adapt<PrescriptionPermissionDTO>();
        }

        public async Task<PrescriptionPermissionDTO> UpdatePermissionAsync(PrescriptionPermissionCUDTO dtocu)
        {
            var model = dtocu.Adapt<PrescriptionPermissionModel>();

            var permissionmodel = _repo.UpdatePermissionInPrescription(model);

            return permissionmodel.Adapt<PrescriptionPermissionDTO>();
        }

        public async Task DeletePermissionAsync(int permissionid, int prescriptionid)
        {
            await _repo.DeletePermissionInPrescription(permissionid, prescriptionid);
        }

    }
}
