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
    public class PermissionRepository : BaseRepository<PermissionModel>, IPermissionRepository
    {
        public PermissionRepository(MediContext context) : base(context)
        {
        }

        public async Task<(List<PermissionModel>, int)> GetByRequest(PermissionRequest request)
        {
            IQueryable<PermissionModel> query = _context.Set<PermissionModel>();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(x => x.Name.Contains(request.Name));
            }

            return await PaginateQuery(query, request);
        }
    }
}