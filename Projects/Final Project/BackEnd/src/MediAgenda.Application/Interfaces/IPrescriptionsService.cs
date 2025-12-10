using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Services
{
    public interface IPrescriptionsService
    {
        Task<PrescriptionAnalysisDTO> AddAnalysisAsync(PrescriptionAnalysisCUDTO dtocu);
        Task<PrescriptionDTO> AddAsync(PrescriptionCreateDTO dtoc);
        Task<PrescriptionMedicineDTO> AddMedicineAsync(PrescriptionMedicineCUDTO dtocu);
        Task<PrescriptionPermissionDTO> AddPermissionAsync(PrescriptionPermissionCUDTO dtocu);
        Task DeleteAnalysisAsync(int analysisid, int prescriptionid);
        Task DeleteMedicineAsync(int medicineid, int prescriptionid);
        Task DeletePermissionAsync(int permissionid, int prescriptionid);
        Task<List<AnalysisDTO>> GetAllAnalysis(int prescriptionid);
        Task<APIResponse<PrescriptionDTO>> GetAllAsync(PrescriptionRequest request);
        Task<List<MedicineDTO>> GetAllMedicine(int prescriptionid);
        Task<List<PermissionDTO>> GetAllPermission(int prescriptionid);
        Task<PrescriptionModel> GetByIdAsync(int id);
        Task<PrescriptionAnalysisDTO> UpdateAnalysisAsync(PrescriptionAnalysisCUDTO dtocu);
        Task<PrescriptionMedicineDTO> UpdateMedicineAsync(PrescriptionMedicineCUDTO dtocu);
        Task<PrescriptionPermissionDTO> UpdatePermissionAsync(PrescriptionPermissionCUDTO dtocu);
        Task DeleteAsync(PrescriptionModel model);
    }
}