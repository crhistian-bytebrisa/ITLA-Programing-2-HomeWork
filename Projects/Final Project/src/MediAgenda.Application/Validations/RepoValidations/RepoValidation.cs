using MediAgenda.Domain.Core;
using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Application.Validations.RepoValidations
{
    public class RepoValidation<T> where T : class
    {
        protected readonly MediContext _context;

        public RepoValidation(MediContext context)
        {
            _context = context;
        }
        public async Task<bool> ExistName(string name)
        {
            return await _context.Set<T>().Cast<IHasName>()
                .AsNoTracking()
                .AnyAsync(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
        }

        public async Task<bool> ExitsTitle(string title)
        {
            return await _context.Set<T>().Cast<IHasTitle>()
                .AnyAsync(x => x.Title.Trim().ToLower() == title.Trim().ToLower());
        }

        public async Task<bool> ExitsUserName(string User)
        {
            return await _context.Set<T>().Cast<IHasUsername>().
                AnyAsync(x => x.UserName.Trim().ToLower() == User.Trim().ToLower());
        }

        public async Task<bool> ExitsEmail(string email)
        {
            return await _context.Set<T>().Cast<IHasEmail>()
                .AnyAsync(x => x.Email.Trim().ToLower() == email.Trim().ToLower());
        }

        public async Task<bool> TimeValidation(DateOnly date, TimeOnly start, TimeOnly end)
        {
            var list = await _context.Set<T>().Cast<IDayValidation>()
                .Where(x => x.Date == date)
                .ToListAsync();

            bool data = list.Where(x => start <= x.EndTime && end >= x.StartTime)
                .Any();

            return data;
        }

    }
}
