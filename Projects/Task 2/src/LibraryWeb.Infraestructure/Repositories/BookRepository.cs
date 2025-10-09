using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Infraestructure.Data.LibraryContext;
using LibraryWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using LibraryWeb.Domain.Interfaces.Repositories;

namespace LibraryWeb.Infraestructure.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .Select(x => x).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Book> AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            book = await GetWithDetailsByIdAsync(book.Id);
            return book;
        }

        public async Task<Book> UpdateAsync(Book book)
        {
            _context.BooksGenres.RemoveRange(_context.BooksGenres.Where(bg => bg.BookId == book.Id));
            _context.BooksLanguages.RemoveRange(_context.BooksLanguages.Where(bl => bl.BookId == book.Id));

            _context.Books.Update(book);
            await _context.SaveChangesAsync();

            book = await GetWithDetailsByIdAsync(book.Id);
            return book;
        }

        public async Task DeleteAsync(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Book>> GetAllWithDetailsAsync()
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.BookLanguages)
                .ThenInclude(bl => bl.Language)
                .ToListAsync();
        }
        
        public async Task<Book?> GetWithDetailsByIdAsync(int id)
        {
            return await _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .Include(b => b.BookGenres)
                .ThenInclude(bg => bg.Genre)
                .Include(b => b.BookLanguages)
                .ThenInclude(bl => bl.Language)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Book?> GetByName(string Title)
        {
            return await _context.Books.FirstOrDefaultAsync(x => x.Title == Title);
        }
    }
}