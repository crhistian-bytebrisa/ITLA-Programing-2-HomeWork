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
            var author = await repo.GetByName(name);

            if (author != null)
            {
                throw new ApplicationException("Ya existe este Autor.");
            }
        }

        private static async Task<Author> GetId(int id, IAuthorRepository repo)
        {
            var author = await repo.GetByIdAsync(id);
            if (author == null)
            {
                throw new ApplicationException("No existe este Autor.");
            }

            return author;
        }

        public static Func<CreateAuthorDTO, IAuthorRepository, Task<Author>> CheckAdd = async (CauthorDTO, repo) =>
        {
            await GetName(CauthorDTO.Name, repo);
            return CauthorDTO.Adapt<Author>();
        };

        public static Func<int,CreateAuthorDTO, IAuthorRepository, Task<Author>> CheckUpdate = async (id,authorDTO, repo) =>
        {
            await GetName(authorDTO.Name, repo);
            await GetId(id, repo);
            var a = authorDTO.Adapt<Author>();
            a.Id = id;
            return a;
        };

        public static Func<int, IAuthorRepository, Task<Author>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
