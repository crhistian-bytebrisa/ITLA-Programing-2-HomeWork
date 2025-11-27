using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class NoteConsultationRepository : BaseRepositoryIdInt<NoteConsultationModel>, INoteConsultationRepository
    {
        public NoteConsultationRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<NoteConsultationModel>, int)> GetAllAsync(NoteConsultationRequest request)
        {
            IQueryable<NoteConsultationModel> query = _context.Set<NoteConsultationModel>();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.Consultation.PatientId == request.PatientId);
            }

            if (request.CreatedFrom is not null)
            {
                query = query.Where(x => x.CreatedAt >= request.CreatedFrom);
            }

            if (request.CreatedTo is not null)
            {
                query = query.Where(x => x.CreatedAt <= request.CreatedTo);
            }

            if (request.UpdatedFrom is not null)
            {
                query = query.Where(x => x.UpdateAt >= request.UpdatedFrom);
            }

            if (request.UpdatedTo is not null)
            {
                query = query.Where(x => x.UpdateAt <= request.UpdatedTo);
            }

            return await query.PaginateAsync(request);
        }
    }
}