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
    public class ValidateAuthor
    {
        private static async Task GetName(string name, IAuthorRepository repo)
        {
            var Author = await repo.GetByName(name);

            if (Author != null)
            {
                throw new ApplicationException("Ya existe este lenguaje.");
            }
        }

        private static async Task<Author> GetId(int id, IAuthorRepository repo)
        {
            var Author = await repo.GetByIdAsync(id);
            if (Author == null)
            {
                throw new ApplicationException("No existe este lenguaje.");
            }

            return Author;
        }

        public static Func<CreateAuthorDTO, IAuthorRepository, Task<Author>> CheckAdd = async (CAuthorDTO, repo) =>
        {
            await GetName(CAuthorDTO.Name, repo);
            return CAuthorDTO.Adapt<Author>();
        };

        public static Func<AuthorDTO, IAuthorRepository, Task<Author>> CheckUpdate = async (AuthorDTO, repo) =>
        {
            await GetName(AuthorDTO.Name, repo);
            await GetId(AuthorDTO.Id, repo);
            return AuthorDTO.Adapt<Author>();
        };

        public static Func<int, IAuthorRepository, Task<Author>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
