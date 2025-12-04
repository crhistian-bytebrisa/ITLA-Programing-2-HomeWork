using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.RequestRepositories.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Core
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        

        protected readonly MediContext _context;
        public BaseRepository(MediContext task)
        {
            _context = task;
        }
        public async Task<(List<T>, int)> GetAllAsync()
        {
            var entities = await _context.Set<T>().AsNoTracking().ToListAsync();
            return (entities, entities.Count);
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
