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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MediAgenda.Infraestructure.Repositories
{
    public class DayAvailableRepository : BaseRepository<DayAvailableModel>, IDayAvailableRepository
    {
        public DayAvailableRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<DayAvailableModel>, int)> GetByRequest(DayAvailableRequest request)
        {
            IQueryable<DayAvailableModel> query = _context.Set<DayAvailableModel>();

            if (request.DateFrom is not null)
            {
                query = query.Where(x => x.Date >= request.DateFrom);
            }

            if (request.DateTo is not null)
            {
                query = query.Where(x => x.Date <= request.DateTo);
            }

            if (request.ClinicId is not null)
            {
                query = query.Where(x => x.ClinicId == request.ClinicId);
            }

            if (request.StartTimeFrom is not null)
            {
                query = query.Where(x => x.StartTime >= request.StartTimeFrom);
            }

            if (request.StartTimeTo is not null)
            {
                query = query.Where(x => x.StartTime <= request.StartTimeTo);
            }

            if (request.OnlyAvailable is true)
            {
                query = query.Where(x => x.Consultations.Count < x.Limit);
            }

            if (request.IncludeConsultations is true)
            {
                query = query.Include(x => x.Consultations);
            }

            if (request.IncludeClinic is true)
            {
                query = query.Include(x => x.Clinic);
            }

            return await PaginateQuery(query, request);
        }
    }
}