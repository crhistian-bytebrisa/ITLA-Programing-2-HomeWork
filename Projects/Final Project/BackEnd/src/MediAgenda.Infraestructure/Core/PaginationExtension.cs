using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Core
{
    public static class PaginationExtensions 
    {
        public static async Task<(List<T> Items, int TotalCount)> PaginateAsync<T>(
            this IQueryable<T> query,
            BaseRequest request) where T : class
        {
            int totalCount = await query.CountAsync();

            int pageSize = request.PageSize.HasValue
                ? Math.Max(1, request.PageSize.Value)
                : 10;

            int page = request.Page.HasValue
                ? Math.Max(1, request.Page.Value)
                : 1;

            List<T> list = await query
                .AsNoTracking()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (list, totalCount);
        }
    }

}
