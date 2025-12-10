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
    public class InsuranceRepository : BaseRepositoryIdInt<InsuranceModel>, IInsuranceRepository
    {
        public InsuranceRepository(MediContext context) : base(context)
        {
        }

        public override Task<InsuranceModel> GetByIdAsync(int id)
        {
            return _context.Set<InsuranceModel>()
                .Include(i => i.Patients)
                .AsNoTracking()
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<(List<InsuranceModel>, int)> GetAllAsync(InsuranceRequest request)
        {
            IQueryable<InsuranceModel> query = _context.Set<InsuranceModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (request.IncludePatientCount is true)
            {
                query = query.Include(x => x.Patients);
            }

            return await query.PaginateAsync(request);
        }
    }
}