using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories;

namespace LibraryWeb.API.Services
{
    public class AuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<Author>> GetAllAsync()
        {
            return await _authorRepository.GetAllAsync();
        }

        public async Task<Author?> GetByIdAsync(int id)
        {
            return await _authorRepository.GetByIdAsync(id);
        }

        public async Task<Author> AddAsync(Author author)
        {
            await _authorRepository.AddAsync(author);
            return author;
        }

        public async Task<Author> UpdateAsync(Author author)
        {
            await _authorRepository.UpdateAsync(author);
            return author;
        }

        public async Task DeleteAsync(int id)
        {
            await _authorRepository.DeleteAsync(id);
        }


    }
}