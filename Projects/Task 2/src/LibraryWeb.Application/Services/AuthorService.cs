using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Application.Interfaces;
using LibraryWeb.Application.Validations;
using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories;
using Mapster;

namespace LibraryWeb.Application.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<List<AuthorDTO>> GetAllAsync()
        {
            var Authors = await _authorRepository.GetAllAsync();
            return Authors.Adapt<List<AuthorDTO>>();
        }

        public async Task<AuthorDTO> GetByIdAsync(int id)
        {
            var Author = await _authorRepository.GetByIdAsync(id);
            return Author.Adapt<AuthorDTO>();
        }

        public async Task<AuthorDTO> AddAsync(CreateAuthorDTO CAuthorDTO)
        {  
            await ValidateAuthor.CheckAdd(CAuthorDTO, _authorRepository);

            var Author = CAuthorDTO.Adapt<Author>();
            await _authorRepository.AddAsync(Author);
            return Author.Adapt<AuthorDTO>();
        }

        public async Task<AuthorDTO> UpdateAsync(int id, CreateAuthorDTO AuthorDTO)
        {         
            var Author = await ValidateAuthor.CheckUpdate(id, AuthorDTO, _authorRepository);

            await _authorRepository.UpdateAsync(Author);
            return Author.Adapt<AuthorDTO>();
        }

        public async Task DeleteAsync(int id)
        {
            var Author = await ValidateAuthor.CheckDelete(id, _authorRepository);
            await _authorRepository.DeleteAsync(Author);
        }


    }
}