using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.API;
using MediAgenda.Application.Interfaces;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Services
{
    public class PatientsService : IPatientsService
    {
        private readonly IPatientRepository _repo;

        public PatientsService(IPatientRepository repo)
        {
            _repo = repo;
        }

        public async Task<PatientModel> GetByIdAsync(int id)
        {
            var entity = await _repo.GetByIdAsync(id);
            return entity;
        }

        public async Task<APIResponse<PatientDTO>> GetAllAsync(PatientRequest request)
        {
            var (list, count) = await _repo.GetAllAsync(request);
            List<PatientDTO> listdto = list.Adapt<List<PatientDTO>>();

            var APIR = new APIResponse<PatientDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }


        public async Task<PatientDTO> AddAsync(PatientCreateDTO dtoc)
        {
            var model = dtoc.Adapt<PatientModel>();
            await _repo.AddAsync(model);
            return model.Adapt<PatientDTO>();
        }

        public async Task UpdateAsync(PatientUpdateDTO dtou)
        {
            var model = dtou.Adapt<PatientModel>();
            await _repo.UpdateAsync(model);
        }

        public async Task DeleteAsync(PatientModel model)
        {
            await _repo.DeleteAsync(model);
        }

        public async Task<APIResponse<NotePatientDTO>> GetAllNotes(int id, PatientNoteRequest request)
        {
            var (list, count) = await _repo.GetAllNotesById(id, request);

            List<NotePatientDTO> listdto = list.Adapt<List<NotePatientDTO>>();

            var APIR = new APIResponse<NotePatientDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<APIResponse<MedicalDocumentDTO>> GetAllMedicalDocuments(int id, PatientMedicalDocumentRequest request)
        {
            var (list, count) = await _repo.GetAllMedicalDocumentsById(id, request);
            List<MedicalDocumentDTO> listdto = list.Adapt<List<MedicalDocumentDTO>>();

            var APIR = new APIResponse<MedicalDocumentDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;

        }

        public async Task<APIResponse<MedicineDTO>> GetAllMedicaments(int id, PatientMedicamentRequest request)
        {
            var (list, count) = await _repo.GetAllMedicamentsById(id, request);
            List<MedicineDTO> listdto = list.Adapt<List<MedicineDTO>>();

            var APIR = new APIResponse<MedicineDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }

        public async Task<APIResponse<ConsultationDTO>> GetAllConsultations(int id, PatientConsultationRequest request)
        {
            var (list, count) = await _repo.GetAllConsultationById(id, request);
            List<ConsultationDTO> listdto = list.Adapt<List<ConsultationDTO>>();

            var APIR = new APIResponse<ConsultationDTO>(listdto, count, request.Page, request.PageSize);
            return APIR;
        }
    }
}
