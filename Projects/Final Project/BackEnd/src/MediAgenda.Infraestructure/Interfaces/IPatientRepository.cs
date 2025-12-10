using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPatientRepository : IBaseRepositoryIdInt<PatientModel>
    {
        Task<(List<PatientModel>, int)> GetAllAsync(PatientRequest request);
        Task<(List<ConsultationModel>, int)> GetAllConsultationById(int id, PatientConsultationRequest request);
        Task<(List<NotePatientModel>, int)> GetAllNotesById(int id, PatientNoteRequest request);
        Task<(List<MedicalDocumentModel>, int)> GetAllMedicalDocumentsById(int id, PatientMedicalDocumentRequest request);
        Task<(List<MedicineModel>, int)> GetAllMedicamentsById(int id, PatientMedicamentRequest request);
    }
}