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
    public class MedicalDocumentRepository : BaseRepositoryIdInt<MedicalDocumentModel>, IMedicalDocumentRepository
    {
        public MedicalDocumentRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<MedicalDocumentModel>, int)> GetAllAsync(MedicalDocumentRequest request)
        {
            IQueryable<MedicalDocumentModel> query = _context.Set<MedicalDocumentModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.FileName.Contains(request.Name));
            }

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.PatientId == request.PatientId);
            }

            if (!string.IsNullOrWhiteSpace(request.Format))
            {
                query = query.Where(x => x.DocumentType == request.Format);
            }

            return await query.PaginateAsync(request);
        }

        public async Task<string> PatientName(int Id)
        {
            return await _context.Set<PatientModel>()
                .Where(p => p.Id == Id)
                .Select(p => p.User.NameComplete)
                .AsNoTracking()
                .FirstOrDefaultAsync();
        }
    }
}
