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
    public class BaseRepositoryIdInt<T> : BaseRepository<T>, IBaseRepositoryIdInt<T> where T : class, IEntityInt
    {
        public BaseRepositoryIdInt(MediContext context) : base(context)
        {

        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
