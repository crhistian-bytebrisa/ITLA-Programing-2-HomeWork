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
    public class ReasonRepository : BaseRepositoryIdInt<ReasonModel>, IReasonRepository
    {
        public ReasonRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<ReasonModel>, int)> GetAllAsync(ReasonRequest request)
        {
            IQueryable<ReasonModel> query = _context.Set<ReasonModel>();
            query = query.Include(x => x.Consultations).AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if(request.Available.HasValue)
            {
                query = query.Where(x => x.Available == request.Available.Value);
            }

            return await query.PaginateAsync(request);
        }

        public async override Task<ReasonModel> GetByIdAsync(int id)
        {
            var entity = await _context.Set<ReasonModel>()
                .Include(x => x.Consultations)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }
    }
}