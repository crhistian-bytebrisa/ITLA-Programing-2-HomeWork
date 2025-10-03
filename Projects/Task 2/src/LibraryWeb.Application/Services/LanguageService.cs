using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.API.Data.LibraryContext;
using LibraryWeb.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.API.Services
{
    public class LanguageService
    {
        private readonly DataContext _context;

        public LanguageService(DataContext context)
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
            if(await LanguageExists(language.Name))
            {
                throw new Exception("Existe un lenguaje con el mismo nombre.");
            }

            await _context.Languages.AddAsync(language);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Language language)
        {
            _context.Languages.Update(language);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Language language)
        {
            _context.Languages.Remove(language);
            await _context.SaveChangesAsync();
        }

        private Task<bool> LanguageExists(string name)
        {
            return _context.Languages.AnyAsync(e => e.Name == name);
        }
        
    }
}