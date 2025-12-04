using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class PatientRepository : BaseRepositoryIdInt<PatientModel>, IPatientRepository
    {
        public PatientRepository(MediContext context) : base(context)
        {
        }

        public override async Task<PatientModel> GetByIdAsync(int id)
        {
            var entity = await _context.Set<PatientModel>()
                .Include(x => x.User)
                .Include(x => x.Insurance)
                .Include(x => x.MedicalDocuments)
                .Include(x => x.Notes)
                .Include(x => x.Consultations)
                .Include(x => x.CurrentMedicaments)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }

        public async Task<(List<PatientModel>, int)> GetAllAsync(PatientRequest request)
        {
            IQueryable<PatientModel> query = _context.Set<PatientModel>();
            query = query.Include(x => x.User)
                         .Include(x => x.Insurance)
                         .Include(x => x.MedicalDocuments)
                         .Include(x => x.Notes)
                         .Include(x => x.Consultations)
                         .Include(x => x.CurrentMedicaments);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.User.NameComplete.Contains(request.Name));
            }

            if (request.OlderAge is not null)
            {
                query = query.Where(x =>
                (DateTime.Today.Year - x.DateOfBirth.Year) -
                (DateTime.Today < x.DateOfBirth.AddYears(DateTime.Today.Year - x.DateOfBirth.Year) ? 1 : 0)
                > request.OlderAge);
            }

            if (request.InsuranceId is not null)
            {
                query = query.Where(x => x.InsuranceId == request.InsuranceId);
            }

            if (!string.IsNullOrWhiteSpace(request.Identification))
            {
                query = query.Where(x => x.Identification == request.Identification);
            }

            if (request.Bloodtype is not null)
            {
                query = query.Where(x => x.Bloodtype == request.Bloodtype);
            }

            if (request.Gender is not null)
            {
                query = query.Where(x => x.Gender == request.Gender);
            }

            return await query.PaginateAsync(request);
        }

        public async Task<(List<ConsultationModel>, int)> GetAllConsultationById(int id, PatientConsultationRequest request)
        {
            var query = _context.Set<ConsultationModel>()
                .Where(x => x.PatientId == id)
                .Include(x => x.Reason)
                .Include(x => x.DayAvailable)
                .Include(x => x.Notes)
                .Include(x => x.Prescriptions)
                .AsNoTracking();

            if (request.State is not null)
            {
                query = query.Where(x => x.State == request.State);
            }

            if (request.DateFrom is not null)
            {
                query = query.Where(x => x.DayAvailable.Date >= request.DateFrom);
            }

            if (request.DateTo is not null)
            {
                query = query.Where(x => x.DayAvailable.Date <= request.DateTo);
            }

            if (request.ClinicId is not null)
            {
                query = query.Where(x => x.DayAvailable.ClinicId == request.ClinicId);
            }

            if (request.ReasonId is not null)
            {
                query = query.Where(x => x.ReasonId == request.ReasonId);
            }

            return await query.PaginateAsync(request);
        }

        public async Task<(List<NotePatientModel>, int)> GetAllNotesById(int id, PatientNoteRequest request)
        {
            var query = _context.Set<NotePatientModel>()
                .Where(x => x.PatientId == id)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if (request.CreatedFrom is not null)
            {
                query = query.Where(x => x.CreatedAt >= request.CreatedFrom);
            }

            if (request.CreatedTo is not null)
            {
                query = query.Where(x => x.CreatedAt <= request.CreatedTo);
            }

            if (request.UpdatedFrom is not null)
            {
                query = query.Where(x => x.UpdateAt >= request.UpdatedFrom);
            }

            if (request.UpdatedTo is not null)
            {
                query = query.Where(x => x.UpdateAt <= request.UpdatedTo);
            }

            return await query.PaginateAsync(request);
        }

        public async Task<(List<MedicalDocumentModel>, int)> GetAllMedicalDocumentsById(int id, PatientMedicalDocumentRequest request)
        {
            var query = _context.Set<MedicalDocumentModel>()
                .Where(x => x.PatientId == id)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.DocumentType))
            {
                query = query.Where(x => x.DocumentType.Contains(request.DocumentType));
            }

            return await query.PaginateAsync(request);
        }

        public async Task<(List<MedicineModel>, int)> GetAllMedicamentsById(int id, PatientMedicamentRequest request)
        {
            var query = _context.Set<MedicineModel>()
                .Where(MedicineModel => MedicineModel.CurrentMedicaments.Any(cm => cm.PatientId == id))
                .AsNoTracking();

           
            if (!string.IsNullOrWhiteSpace(request.MedicamentName))
            {
                query = query.Where(x => x.Name.Contains(request.MedicamentName));
            }

            if (request.IsCurrent is not null)
            {
                if (request.IsCurrent.Value)
                {
                    query = query.Where(x => x.CurrentMedicaments.Any(x => x.EndMedication > DateOnly.FromDateTime(DateTime.Now)));
                }
            }

            return await query.PaginateAsync(request);
        }
    }
}