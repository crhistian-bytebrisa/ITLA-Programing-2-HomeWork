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
    public class MedicineRepository : BaseRepository<MedicineModel>, IMedicineRepository
    {
        public MedicineRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<MedicineModel>, int)> GetByRequest(MedicineRequest request)
        {
            IQueryable<MedicineModel> query = _context.Set<MedicineModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.Format))
            {
                query = query.Where(x => x.Format == request.Format);
            }

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.CurrentMedicaments.Any(cm => cm.PatientId == request.PatientId));
            }

            if (request.IncludePrescriptions is true)
            {
                query = query.Include(x => x.PrescriptionMedicines)
                    .ThenInclude(pm => pm.Prescription);
            }

            if (request.IncludeCurrentMedicaments is true)
            {
                query = query.Include(x => x.CurrentMedicaments);
            }

            return await PaginateQuery(query, request);
        }
    }
}