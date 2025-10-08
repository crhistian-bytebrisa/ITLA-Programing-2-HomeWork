using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories;
using Mapster;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.Validations
{
    public class ValidateGenre
    {
        private static async Task GetName(string name, IGenreRepository repo)
        {
            var genre = await repo.GetByName(name);

            if (genre != null)
            {
                throw new ApplicationException("Ya existe este lenguaje.");
            }
        }

        private static async Task<Genre> GetId(int id, IGenreRepository repo)
        {
            var genre = await repo.GetByIdAsync(id);
            if (genre == null)
            {
                throw new ApplicationException("No existe este lenguaje.");
            }

            return genre;
        }

        public static Func<CreateGenreDTO, IGenreRepository, Task<Genre>> CheckAdd = async (CgenreDTO, repo) =>
        {
            await GetName(CgenreDTO.Name, repo);
            return CgenreDTO.Adapt<Genre>();
        };

        public static Func<GenreDTO, IGenreRepository, Task<Genre>> CheckUpdate = async (genreDTO, repo) =>
        {
            await GetName(genreDTO.Name, repo);
            await GetId(genreDTO.Id, repo);
            return genreDTO.Adapt<Genre>();
        };

        public static Func<int, IGenreRepository, Task<Genre>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
