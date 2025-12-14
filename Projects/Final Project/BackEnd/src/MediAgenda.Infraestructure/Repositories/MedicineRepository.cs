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
    public class MedicineRepository : BaseRepositoryIdInt<MedicineModel>, IMedicineRepository
    {
        public MedicineRepository(MediContext context) : base(context)
        {
        }

        public override Task<MedicineModel> GetByIdAsync(int id)
        {
            return _context.Set<MedicineModel>()
                .Include(x => x.PrescriptionMedicines)
                .Include(x => x.CurrentMedicaments)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<(List<MedicineModel>, int)> GetAllAsync(MedicineRequest request)
        {
            IQueryable<MedicineModel> query = _context.Set<MedicineModel>();

            query = query
                .Include(x => x.PrescriptionMedicines)
                .Include(x => x.CurrentMedicaments);

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            if (!string.IsNullOrWhiteSpace(request.Format))
            {
                query = query.Where(x => x.Format == request.Format);
            }
                       

            return await query.PaginateAsync(request);
        }
    }
}