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
    public class NotePatientRepository : BaseRepositoryIdInt<NotePatientModel>, INotePatientRepository
    {
        public NotePatientRepository(MediContext context) : base(context)
        {
        }

        public override Task<NotePatientModel> GetByIdAsync(int id)
        {
            return _context.Set<NotePatientModel>()
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(List<NotePatientModel>, int)> GetAllAsync(NotePatientRequest request)
        {
            IQueryable<NotePatientModel> query = _context.Set<NotePatientModel>();

            if (!string.IsNullOrWhiteSpace(request.Title))
            {
                query = query.Where(x => x.Title.Contains(request.Title));
            }

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.Patient.Id == request.PatientId);
            }

            if (request.IncludePatient)
            {
                query = query.Include(x => x.Patient);
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