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

            if (request.IncludePrescriptions is true)
            {
                query = query.Include(x => x.Prescriptions);
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
    }
}