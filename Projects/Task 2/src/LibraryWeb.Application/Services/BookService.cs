using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.API.Entities;
using LibraryWeb.API.Repositories;

namespace LibraryWeb.API.Services
{
    public class BookService
    {
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<Book>> GetAllAsync()
        {
            return await _bookRepository.GetAllAsync();
        }

        public async Task<Book?> GetBookByIdAsync(int id)
        {
            return await _bookRepository.GetByIdAsync(id);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            await _bookRepository.AddAsync(book);
            return book;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            await _bookRepository.UpdateAsync(book);
            return book;
        }

        public async Task DeleteBookAsync(int id)
        {
            await _bookRepository.DeleteAsync(id);
        }

        public async Task<List<Book>> GetAllBooksWithDetailsAsync()
        {
            return await _bookRepository.GetAllBooksWithDetailsAsync();
        }

        public async Task<Book?> GetBooksByAuthorIdAsync(int authorId)
        {
            return await _bookRepository.GetBookWithDetailsByIdAsync(authorId);
        }
    }
}