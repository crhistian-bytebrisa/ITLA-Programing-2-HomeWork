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
    public class MedicalDocumentRepository : BaseRepository<MedicalDocumentModel>, IMedicalDocumentRepository
    {
        public MedicalDocumentRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<MedicalDocumentModel>, int)> GetByRequest(MedicalDocumentRequest request)
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

            if (request.IncludePatient is true)
            {
                query = query.Include(x => x.Patient);
            }

            return await PaginateQuery(query, request);
        }
    }
}
