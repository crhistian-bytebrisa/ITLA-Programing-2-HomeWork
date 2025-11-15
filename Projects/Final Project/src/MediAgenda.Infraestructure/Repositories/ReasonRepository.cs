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
    public class ReasonRepository : BaseRepository<ReasonModel>, IReasonRepository
    {
        public ReasonRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<ReasonModel>, int)> GetByRequest(ReasonRequest request)
        {
            IQueryable<ReasonModel> query = _context.Set<ReasonModel>();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if(request.Available.HasValue)
            {
                query = query.Where(x => x.Available == request.Available.Value);
            }

            if (request.IncludeConsultations is true)
            {
                query = query.Include(x=> x.Consultations);
            }

            return await PaginateQuery(query, request);
        }
    }
}