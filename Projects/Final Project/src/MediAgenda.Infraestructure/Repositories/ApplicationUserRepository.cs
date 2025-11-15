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
    public class ApplicationUserRepository : BaseRepository<ApplicationUserModel>, IApplicationUserRepository
    {
        public ApplicationUserRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<ApplicationUserModel>, int)> GetByRequest(ApplicationUserRequest request)
        {
            IQueryable<ApplicationUserModel> query = _context.Set<ApplicationUserModel>();

            if (!string.IsNullOrWhiteSpace(request.FullName))
            {
                query = query.Where(x => x.NameComplete.Contains(request.FullName));
            }

            return await PaginateQuery(query, request);
        }
    }
}