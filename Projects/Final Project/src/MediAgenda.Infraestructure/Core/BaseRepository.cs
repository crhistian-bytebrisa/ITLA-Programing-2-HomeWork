using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        protected async Task<(List<T>, int)> PaginateQuery(IQueryable<T> query,BaseRequest request)
        {
            int totalcount = await query.CountAsync();

            int pageSize = 10;
            int page = 1;

            if (request.PageSize != null)
            {
                pageSize = Math.Max(1, request.PageSize.Value);
            }

            if (request.Page != null)
            {
                page = Math.Max(1, request.Page.Value);
            }

            int totalPages = (int)Math.Ceiling((double)totalcount / pageSize);
            
            List<T> list = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .AsNoTracking()
                    .ToListAsync();

            return (list, totalcount);
        }

        protected readonly MediContext _context;
        public BaseRepository(MediContext task)
        {
            _context = task;
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = _context.Set<T>().Find(id);
            return entity;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var entities = await _context.Set<T>().ToListAsync();
            return entities;
        }

        public async Task<T> AddAsync(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            _context.SaveChanges();
            return entity;
        }

        public async Task DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            _context.SaveChanges();
        }
    }
}
