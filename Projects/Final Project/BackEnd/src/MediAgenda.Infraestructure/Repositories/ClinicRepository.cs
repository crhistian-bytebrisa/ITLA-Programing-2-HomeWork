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
    public class ClinicRepository : BaseRepositoryIdInt<ClinicModel>, IClinicRepository
    {
        public ClinicRepository(MediContext context) : base(context)
        {

        }

        public async Task<(List<ClinicModel>, int)> GetAllAsync(ClinicRequest request)
        {
            IQueryable<ClinicModel> query = _context.Set<ClinicModel>()
                .Include(x=> x.DaysAvailable).AsNoTracking();

            if(!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            return await query.PaginateAsync(request);
        }

        public override async Task<ClinicModel> GetByIdAsync(int id)
        {
            var entity = await _context.Set<ClinicModel>()
                .Include(x => x.DaysAvailable)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity; 
        }

        public async Task<(List<DayAvailableModel>,int)> GetAllDaysAvailableById(int id, ClinicDaysAvailableRequest request)
        {
            IQueryable<DayAvailableModel> query = _context.Set<DayAvailableModel>()
                .Where(x => x.ClinicId == id)
                .AsNoTracking();

            if (request.DateFrom is not null)
            {
                query = query.Where(x => x.Date >= request.DateFrom);
            }

            if (request.DateTo is not null)
            {
                query = query.Where(x => x.Date <= request.DateTo);
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

            return await query.PaginateAsync(request);
        }
    }
}