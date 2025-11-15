using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models.Relations;
using MediAgenda.Infraestructure.RequestRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class CurrentMedicamentRepository : BaseRepository<CurrentMedicamentsModel>, ICurrentMedicamentRepository
    {
        public CurrentMedicamentRepository(MediContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<CurrentMedicamentsModel>> GetByRequest(CurrentMedicamentRequest request)
        {
            IQueryable<CurrentMedicamentsModel> query = _context.Set<CurrentMedicamentsModel>();

            if (request.PatientId is not null)
            {
                query = query.Where(x => x.PatientId == request.PatientId);
            }

            if (request.MedicineId is not null)
            {
                query = query.Where(x => x.MedicineId == request.MedicineId);
            }

            if (request.StartMedicationFrom is not null)
            {
                query = query.Where(x => x.StartMedication >= request.StartMedicationFrom);
            }

            if (request.StartMedicationTo is not null)
            {
                query = query.Where(x => x.StartMedication <= request.StartMedicationTo);
            }

            if (request.IsActive is not null)
            {
                if (request.IsActive is true)
                {
                    query = query.Where(x => x.EndMedication == null || x.EndMedication >= DateOnly.FromDateTime(DateTime.Today));
                }
                else
                {
                    query = query.Where(x => x.EndMedication != null && x.EndMedication < DateOnly.FromDateTime(DateTime.Today));
                }
            }

            if (request.IncludePatient is true)
            {
                query = query.Include(x => x.Patient);
            }

            if (request.IncludeMedicine is true)
            {
                query = query.Include(x => x.Medicine);
            }

            return await query.ToListAsync();
        }
    }
}
