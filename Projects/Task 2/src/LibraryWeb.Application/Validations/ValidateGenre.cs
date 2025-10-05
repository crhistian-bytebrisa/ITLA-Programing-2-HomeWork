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
            var Genre = await repo.GetByName(name);

            if (Genre != null)
            {
                throw new ApplicationException("Ya existe este lenguaje.");
            }
        }

        private static async Task<Genre> GetId(int id, IGenreRepository repo)
        {
            var Genre = await repo.GetByIdAsync(id);
            if (Genre == null)
            {
                throw new ApplicationException("No existe este lenguaje.");
            }

            return Genre;
        }

        public static Func<CreateGenreDTO, IGenreRepository, Task<Genre>> CheckAdd = async (CGenreDTO, repo) =>
        {
            await GetName(CGenreDTO.Name, repo);
            return CGenreDTO.Adapt<Genre>();
        };

        public static Func<GenreDTO, IGenreRepository, Task<Genre>> CheckUpdate = async (GenreDTO, repo) =>
        {
            await GetName(GenreDTO.Name, repo);
            await GetId(GenreDTO.Id, repo);
            return GenreDTO.Adapt<Genre>();
        };

        public static Func<int, IGenreRepository, Task<Genre>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
