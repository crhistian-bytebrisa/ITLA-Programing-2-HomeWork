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
    public class ClinicRepository : BaseRepository<ClinicModel>, IClinicRepository
    {
        public ClinicRepository(MediContext context) : base(context)
        {

        }

        public async Task<(List<ClinicModel>, int)> GetByRequest(ClinicRequest request)
        {
            IQueryable<ClinicModel> query = _context.Set<ClinicModel>();

            if(!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if(request.IncludeDayAvalilable is true)
            {
                query = query.Include(x => x.DaysAvailable);
            }

            return await PaginateQuery(query, request);
        }
    }
}