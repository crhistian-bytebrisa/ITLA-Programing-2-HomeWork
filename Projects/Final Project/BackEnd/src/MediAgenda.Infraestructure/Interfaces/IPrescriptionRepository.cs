using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Relations;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Repositories
{
    public interface IPrescriptionRepository : IBaseRepository<PrescriptionModel>
    {
        Task<PrescriptionAnalysisModel> AddAnalysisInPrescription(PrescriptionAnalysisModel entity);
        Task<PrescriptionModel> AddAsync(PrescriptionModel entity);
        Task<PrescriptionMedicineModel> AddMedicineInPrescription(PrescriptionMedicineModel entity);
        Task<PrescriptionPermissionModel> AddPermissionInPrescription(PrescriptionPermissionModel entity);
        Task DeleteAnalysisInPrescription(int AnalysisId, int PrescriptionId);
        Task DeleteMedicineInPrescription(int MedicineId, int PrescriptionId);
        Task DeletePermissionInPrescription(int PermissionId, int PrescriptionId);
        Task<(List<PrescriptionModel>, int)> GetAllAsync(PrescriptionRequest request);
        Task<(List<AnalysisModel>, int)> GetAnalysisInPrescription(int PrescriptionId);
        Task<(List<MedicineModel>, int)> GetMedicineInPrescription(int PrescriptionId);
        Task<(List<PermissionModel>, int)> GetPermissionInPrescription(int PrescriptionId);
        Task<PrescriptionModel> GetByIdAsync(int id);
        Task PrintPrescription(int id);
        Task<PrescriptionAnalysisModel> UpdateAnalysisInPrescription(PrescriptionAnalysisModel entity);
        Task<PrescriptionMedicineModel> UpdateMedicineInPrescription(PrescriptionMedicineModel entity);
        Task<PrescriptionPermissionModel> UpdatePermissionInPrescription(PrescriptionPermissionModel entity);
    }
}