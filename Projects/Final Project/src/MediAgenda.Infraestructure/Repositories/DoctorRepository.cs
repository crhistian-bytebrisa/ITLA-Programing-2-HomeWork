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
    public class DoctorRepository : BaseRepositoryIdInt<DoctorModel>, IDoctorRepository
    {
        public DoctorRepository(MediContext context) : base(context)
        {
        }

        public override Task<DoctorModel> GetByIdAsync(int id)
        {
            return _context.Set<DoctorModel>()
                .Include(d => d.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<(List<DoctorModel>, int)> GetAllAsync(DoctorRequest request)
        {
            IQueryable<DoctorModel> query = _context.Set<DoctorModel>();

            return await query.PaginateAsync(request);
        }

        
    }
}