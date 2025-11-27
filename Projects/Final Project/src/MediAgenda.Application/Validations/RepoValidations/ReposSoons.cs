using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.RepoValidations
{
    public class RepoIdStringValidation<T> : RepoValidation<T> where T : class, IEntityString
    {
        public RepoIdStringValidation(MediContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var exist = await _context.Set<T>().AsNoTracking().AnyAsync(x => x.Id == id);
            return exist;
        }
    }

    public class RepoIdIntValidation<T> : RepoValidation<T> where T : class, IEntityInt
    {
        public RepoIdIntValidation(MediContext context) : base(context)
        {
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var exist = await _context.Set<T>().AsNoTracking().AnyAsync(x => x.Id == id);
            return exist;
        }
    }
}
