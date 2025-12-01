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
    public class NoteConsultationRepository : BaseRepositoryIdInt<NoteConsultationModel>, INoteConsultationRepository
    {
        public NoteConsultationRepository(MediContext context) : base(context)
        {
        }

        public override Task<NoteConsultationModel> GetByIdAsync(int id)
        {
            return _context.Set<NoteConsultationModel>()
                .Include(x => x.Consultation)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(List<NoteConsultationModel>, int)> GetAllAsync(NoteConsultationRequest request)
        {
            IQueryable<NoteConsultationModel> query = _context.Set<NoteConsultationModel>();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if (request.ConsultationId is not null)
            {
                query = query.Where(x => x.Consultation.Id == request.ConsultationId);
            }

            if (request.IncludeConsultation)
            {
                query = query.Include(x => x.Consultation);
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