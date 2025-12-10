using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Application.Interfaces
{
    public interface IPatientsService
    {
        Task<PatientDTO> AddAsync(PatientCreateDTO dtoc);
        Task DeleteAsync(PatientModel model);
        Task<APIResponse<PatientDTO>> GetAllAsync(PatientRequest request);
        Task<APIResponse<MedicalDocumentDTO>> GetAllMedicalDocuments(int id, PatientMedicalDocumentRequest request);
        Task<APIResponse<MedicineDTO>> GetAllMedicaments(int id, PatientMedicamentRequest request);
        Task<APIResponse<NotePatientDTO>> GetAllNotes(int id, PatientNoteRequest request);
        Task<APIResponse<ConsultationDTO>> GetAllConsultations(int id, PatientConsultationRequest request);
        Task<PatientModel> GetByIdAsync(int id);
        Task UpdateAsync(PatientUpdateDTO dtou);
    }
}