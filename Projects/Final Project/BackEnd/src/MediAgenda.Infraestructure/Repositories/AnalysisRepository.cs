using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class AnalysisRepository : BaseRepositoryIdInt<AnalysisModel>, IAnalysisRepository
    {
        public AnalysisRepository(MediContext context) : base(context)
        {

        }

        public async Task<(List<AnalysisModel>, int)> GetAllAsync(AnalysisRequest request)
        {
            IQueryable<AnalysisModel> query = _context.Set<AnalysisModel>();

            query = query.Include(x => x.PrescriptionAnalyses)
                    .ThenInclude(pa => pa.Prescription)
                    .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            return await query.PaginateAsync(request);

        }

        public override async Task<AnalysisModel> GetByIdAsync(int id)
        {
            var entity = await _context.Set<AnalysisModel>()
                .Include(x => x.PrescriptionAnalyses)
                    .ThenInclude(pa => pa.Prescription)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }
    }
}
