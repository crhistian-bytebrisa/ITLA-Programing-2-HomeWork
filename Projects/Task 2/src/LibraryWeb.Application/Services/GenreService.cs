using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryWeb.API.Entities;
using LibraryWeb.API.Repositories;

namespace LibraryWeb.API.Services
{
    public class GenreService
    {
        private readonly GenreRepository _genreRepository;

        public GenreService(GenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<List<Genre>> GetAllAsync()
        {
            return await _genreRepository.GetAllAsync();
        }

        public async Task<Genre?> GetByIdAsync(int id)
        {
            return await _genreRepository.GetByIdAsync(id);
        }

        public async Task<Genre> AddAsync(Genre genre)
        {
            await _genreRepository.AddAsync(genre);
            return genre;
        }

        public async Task<Genre> UpdateAsync(Genre genre)
        {
            await _genreRepository.UpdateAsync(genre);
            return genre;
        }

        public async Task DeleteAsync(int id)
        {
            await _genreRepository.DeleteAsync(id);
        }
    }
}