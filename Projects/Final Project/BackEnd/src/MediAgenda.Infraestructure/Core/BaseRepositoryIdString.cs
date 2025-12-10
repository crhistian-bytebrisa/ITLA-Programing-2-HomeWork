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
    public class BaseRepositoryIdString<T> : BaseRepository<T>, IBaseRepositoryIdString<T> where T : class, IEntityString
    {
        public BaseRepositoryIdString(MediContext context) : base(context)
        {

        }

        public virtual async Task<T> GetByIdAsync(string id)
        {
            var entity = await _context.Set<T>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
    }
}
