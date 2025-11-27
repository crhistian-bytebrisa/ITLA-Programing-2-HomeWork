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
    public class NotePatientRepository : BaseRepositoryIdInt<NotePatientModel>, INotePatientRepository
    {
        public NotePatientRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<NotePatientModel>, int)> GetAllAsync(NotePatientRequest request)
        {
            IQueryable<NotePatientModel> query = _context.Set<NotePatientModel>();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
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

            if (!string.IsNullOrWhiteSpace(request.SearchTerm))
            {
                query = query.Where(x => x.Title.Contains(request.SearchTerm) || x.Content.Contains(request.SearchTerm));
            }

            return await query.PaginateAsync(request);
        }
    }
}