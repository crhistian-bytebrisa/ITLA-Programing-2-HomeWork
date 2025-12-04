using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class ApplicationUserRepository : BaseRepositoryIdString<ApplicationUserModel>, IApplicationUserRepository
    {

        public ApplicationUserRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<ApplicationUserModel>, int)> GetAllAsync(ApplicationUserRequest request)
        {
            IQueryable<ApplicationUserModel> query = _context.Set<ApplicationUserModel>();
            query = query.Include(x => x.Doctor)
                .Include(x => x.Patient)
                .AsNoTracking();

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                query = query.Where(x => x.NameComplete.Contains(request.FullName));
            }

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                query = query.Where(x => x.Email != null && x.Email.Contains(request.Email));
            }

            return await query.PaginateAsync(request);
        }

        public override async Task<ApplicationUserModel> GetByIdAsync(string id)
        {
            var entity = await _context.Set<ApplicationUserModel>()
                .Include(x => x.Doctor)
                .Include(x => x.Patient)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }


    }
}