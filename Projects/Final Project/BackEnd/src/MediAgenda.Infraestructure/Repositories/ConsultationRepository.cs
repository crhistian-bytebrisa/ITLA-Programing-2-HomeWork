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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediAgenda.Infraestructure.Repositories
{
    public class ConsultationRepository : BaseRepositoryIdInt<ConsultationModel>, IConsultationRepository
    {
        public ConsultationRepository(MediContext context) : base(context)
        {
        }

        public override async Task<ConsultationModel> AddAsync(ConsultationModel entity)
        {
            var day = await _context.DaysAvailable.FirstOrDefaultAsync(x => x.Id == entity.DayAvailableId);
            entity.Turn = day.Consultations.Count + 1;
            return await base.AddAsync(entity);
        }

        public override async Task<ConsultationModel> GetByIdAsync(int id)
        {
            return await _context.Set<ConsultationModel>()
                .Include(x => x.Prescription)
                .Include(x => x.Notes)
                .Include(x => x.Patient)
                .Include(x => x.DayAvailable)
                .Include(x => x.Reason)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(List<ConsultationModel>, int)> GetAllAsync(ConsultationRequest request)
        {
            IQueryable<ConsultationModel> query = _context.Set<ConsultationModel>();

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.PatientId == request.PatientId);
            }

            if (request.ReasonId is not null)
            {
                query = query.Where(x => x.ReasonId == request.ReasonId);
            }

            if (request.DayAvailableId is not null)
            {
                query = query.Where(x => x.DayAvailableId == request.DayAvailableId);
            }

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

            if (request.IncludeNote is true)
            {
                query = query.Include(x => x.Notes);
            }

            if (request.IncludePrescription is true)
            {
                query = query.Include(x => x.Prescription);
            }

            if (request.IncludePatient is true)
            {
                query = query.Include(x => x.Patient);
            }

            if (request.IncludeReason is true)
            {
                query = query.Include(x => x.Reason);
            }

            if (request.IncludeDayAvailable is true)
            {
                query = query.Include(x => x.DayAvailable);
            }

            return await query.PaginateAsync(request);
        }

        public async Task<(List<NoteConsultationModel>, int)> GetAllNotesById(int id, ConsultationNoteRequest request)
        {
            var query = _context.Set<NoteConsultationModel>()
                .Where(x => x.ConsultationId == id)
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
    }
}