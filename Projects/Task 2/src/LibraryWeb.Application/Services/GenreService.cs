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
    public class GenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<GenreDTO>> GetAllAsync()
        {
            var books = await _genreRepository.GetAllAsync();
            return books.Adapt<List<GenreDTO>>();
        }

        public async Task<GenreDTO> GetByIdAsync(int id)
        {
            var book = await _genreRepository.GetByIdAsync(id);
            return book.Adapt<GenreDTO>();
        }

        public async Task<GenreDTO> AddAsync(CreateGenreDTO CgenreDTO)
        {
            await ValidateGenre.CheckAdd(CgenreDTO, _genreRepository);

            var genre = CgenreDTO.Adapt<Genre>();
            await _genreRepository.AddAsync(genre);
            var genreDTO = genre.Adapt<GenreDTO>();
            return genreDTO;
        }

        public async Task<GenreDTO> UpdateAsync(GenreDTO genreDTO)
        {
            await ValidateGenre.CheckUpdate(genreDTO, _genreRepository);

            var genre = genreDTO.Adapt<Genre>();
            await _genreRepository.UpdateAsync(genre);
            genreDTO = genre.Adapt<GenreDTO>();
            return genreDTO;
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await ValidateGenre.CheckDelete(id, _genreRepository);
            await _genreRepository.DeleteAsync(genre);
        }
    }
}