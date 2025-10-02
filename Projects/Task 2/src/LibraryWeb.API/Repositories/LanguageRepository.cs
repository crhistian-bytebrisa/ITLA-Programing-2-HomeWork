using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.API.Data.LibraryContext;
using LibraryWeb.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.API.Repositories
{
    public class LanguageRepository
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

        public async Task AddAsync(Language language)
        {
            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Language language)
        {
            _context.Languages.Update(language);
            await _context.SaveChangesAsync();
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