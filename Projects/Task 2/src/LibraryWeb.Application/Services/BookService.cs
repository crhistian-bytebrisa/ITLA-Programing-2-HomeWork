using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Application.Validations;
using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories;
using Mapster;

namespace LibraryWeb.Application.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<List<BookDTO>> GetAllAsync()
        {
            var Books = await _bookRepository.GetAllAsync();
            return Books.Adapt<List<BookDTO>>();
        }

        public async Task<BookDTO> GetBookByIdAsync(int id)
        {
            var Book = await _bookRepository.GetByIdAsync(id);
            return Book.Adapt<BookDTO>();   
        }

        public async Task<BookDTO> AddBookAsync(CreateBookDTO CbookDTO)
        {
            await ValidateBook.CheckAdd(CbookDTO, _bookRepository);

            var book = CbookDTO.Adapt<Book>();
            await _bookRepository.AddAsync(book);
            return book.Adapt<BookDTO>();
        }

        public async Task<BookDTO> UpdateBookAsync(BookDTO bookDTO)
        {
            await ValidateBook.CheckUpdate(bookDTO, _bookRepository);

            var book = bookDTO.Adapt<Book>();
            await _bookRepository.UpdateAsync(book);
            return book.Adapt<BookDTO>();
        }

        public async Task DeleteBookAsync(int id)
        {
            var book = await ValidateBook.CheckDelete(id, _bookRepository);
            await _bookRepository.DeleteAsync(book);
        }

        public async Task<List<BookDTO>> GetAllBooksWithDetailsAsync()
        {
            var books = await _bookRepository.GetAllWithDetailsAsync();
            return books.Adapt<List<BookDTO>>();
        }

        public async Task<BookDTO> GetBooksByAuthorIdAsync(int authorId)
        {
            var book = await _bookRepository.GetWithDetailsByIdAsync(authorId);
            return book.Adapt<BookDTO>();
        }
    }
}