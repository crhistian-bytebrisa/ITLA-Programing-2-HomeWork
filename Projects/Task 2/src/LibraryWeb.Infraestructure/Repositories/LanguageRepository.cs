using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Infraestructure.Data.LibraryContext;
using LibraryWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Domain.Interfaces.Repositories.Base;
using LibraryWeb.Domain.Interfaces.Repositories;

namespace LibraryWeb.API.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly DataContext _context;

        public LanguageRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Language>> GetAllAsync()
        {
            return await _context.Languages.Select(x => x).ToListAsync();
        }

        public async Task<Language?> GetByIdAsync(int id)
        {
            return await _context.Languages.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Language> AddAsync(Language language)
        {
            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task<Language> UpdateAsync(Language language)
        {
            _context.Languages.Update(language);
            await _context.SaveChangesAsync();
            return language;
        }

        public async Task DeleteAsync(int id)
        {
            var language = await GetByIdAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
                await _context.SaveChangesAsync();
            }
        }
    }
}