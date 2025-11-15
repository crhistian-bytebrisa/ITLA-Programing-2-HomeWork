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
    public class PrescriptionRepository : BaseRepository<PrescriptionModel>, IPrescriptionRepository
    {
        public PrescriptionRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<PrescriptionModel>, int)> GetByRequest(PrescriptionRequest request)
        {
            IQueryable<PrescriptionModel> query = _context.Set<PrescriptionModel>();

            if (request.ConsultationId is not null)
            {
                query = query.Where(x => x.ConsultationId == request.ConsultationId);
            }

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.Consultation.PatientId == request.PatientId);
            }

            if (request.CreatedFrom is not null)
            {
                query = query.Where(x => x.CreatedAt >= request.CreatedFrom);
            }

            if (request.CreatedTo is not null)
            {
                query = query.Where(x => x.CreatedAt <= request.CreatedTo);
            }

            if (request.LastPrintFrom is not null)
            {
                query = query.Where(x => x.LastPrint >= request.LastPrintFrom);
            }

            if (request.LastPrintTo is not null)
            {
                query = query.Where(x => x.LastPrint <= request.LastPrintTo);
            }

            if (request.IncludeMedicines is true)
            {
                query = query.Include(x => x.PrescriptionMedicines)
                    .ThenInclude(pm => pm.Medicine);
            }

            if (request.IncludeAnalysis is true)
            {
                query = query.Include(x => x.PrescriptionAnalysis)
                    .ThenInclude(pa => pa.Analysis);
            }

            if (request.IncludePermissions is true)
            {
                query = query.Include(x => x.PrescriptionPermissions)
                    .ThenInclude(pp => pp.Permission);
            }

            if (request.IncludeConsultation is true)
            {
                query = query.Include(x => x.Consultation);
            }

            return await PaginateQuery(query, request);
        }
    }
}