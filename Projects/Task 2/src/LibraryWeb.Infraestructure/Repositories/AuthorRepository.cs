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
    public class AuthorRepostory : IAuthorRepository
    {
        private readonly DataContext _context;

        public AuthorRepostory(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _context.Authors.Select(x => x).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return author;

        }

        public async Task DeleteAsync(Author author)
        {
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
        
        public async Task<Author?> GetByName(string name)
        {
            return await _context.Authors.FirstOrDefaultAsync(x => x.Name == name);
        }
    }
}