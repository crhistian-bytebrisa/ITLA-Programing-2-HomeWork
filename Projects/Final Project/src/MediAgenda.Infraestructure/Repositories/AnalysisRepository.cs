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
    public class AnalysisRepository : BaseRepository<AnalysisModel>, IAnalysisRepository
    {
        public AnalysisRepository(MediContext context) : base(context)
        {

        }

        public async Task<(List<AnalysisModel>, int)> GetByRequest(AnalysisRequest request)
        {
            IQueryable<AnalysisModel> query = _context.Set<AnalysisModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (request.PatientId is not null)
            {
                query = query
                    .Where(x => x.PrescriptionAnalyses
                    .Any(pa => pa.Prescription.Consultation.PatientId == request.PatientId));
            }

            if (request.IncludePrescription is true)
            {
                query = query.Include(x => x.PrescriptionAnalyses)
                    .ThenInclude(pa => pa.Prescription);
            }

            return await PaginateQuery(query, request);

        }


    }
}
