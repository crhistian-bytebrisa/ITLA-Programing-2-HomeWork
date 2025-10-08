using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Infraestructure.Data.LibraryContext;
using LibraryWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Domain.Interfaces.Repositories.Base;
using LibraryWeb.Domain.Interfaces.Repositories;

namespace LibraryWeb.Infraestructure.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly DataContext _context;
        public GenreRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _context.Genres
                .AsNoTracking()
                .Select(x => x).ToListAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _context.Genres.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task<Genre> UpdateAsync(Genre genre)
        {
            _context.Genres.Update(genre);
            await _context.SaveChangesAsync();
            return genre;
        }

        public async Task DeleteAsync(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<Genre?> GetByName(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(x => x.Name == name);
        }

    }
}