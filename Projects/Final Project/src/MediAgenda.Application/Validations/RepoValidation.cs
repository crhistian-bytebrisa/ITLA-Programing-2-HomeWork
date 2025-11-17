using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations
{
    public class RepoValidation<T> where T : class
    {
        private readonly MediContext _context;

        public RepoValidation(MediContext context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity != null;
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity != null;
        }

        public async Task<bool> ExistName(string name)
        {
            var have = typeof(T).GetProperty("Name");
            if (have == null)
            {
                throw new Exception("Se intento verificar una propiedad que no existe.");
            }

            return await _context.Set<T>().Cast<IHasName>().AnyAsync(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<bool> ExitsTitle(string title)
        {
            var have = typeof(T).GetProperty("Title");
            if (have == null)
            {
                throw new Exception("Se intento verificar una propiedad que no existe.");
            }
            
            return await _context.Set<T>().Cast<IHasTitle>().AnyAsync(x => x.Title.Trim().ToLower() == title.Trim().ToLower());
        }

        public async Task<bool> ExitsUserName(string User)
        {
            var have = typeof(T).GetProperty("UserName");
            if (have == null)
            {
                throw new Exception("Se intento verificar una propiedad que no existe.");
            }

            return await _context.Set<T>().Cast<IHasUsername>().AnyAsync(x => x.UserName.Trim().ToLower() == User.Trim().ToLower());
        }

    }
}
