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
    public class InsuranceRepository : BaseRepository<InsuranceModel>, IInsuranceRepository
    {
        public InsuranceRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<InsuranceModel>, int)> GetByRequest(InsuranceRequest request)
        {
            IQueryable<InsuranceModel> query = _context.Set<InsuranceModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (request.IncludePatient is true)
            {
                query = query.Include(x => x.Patients);
            }

            return await PaginateQuery(query, request);
        }
    }
}