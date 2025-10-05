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
    public class ValidateBook
    {
        private static async Task GetName(string name, IBookRepository repo)
        {
            var Book = await repo.GetByName(name);

            if (Book != null)
            {
                throw new ApplicationException("Ya existe este lenguaje.");
            }
        }

        private static async Task<Book> GetId(int id, IBookRepository repo)
        {
            var Book = await repo.GetByIdAsync(id);
            if (Book == null)
            {
                throw new ApplicationException("No existe este lenguaje.");
            }

            return Book;
        }

        public static Func<CreateBookDTO, IBookRepository, Task<Book>> CheckAdd = async (CBookDTO, repo) =>
        {
            await GetName(CBookDTO.Title, repo);
            return CBookDTO.Adapt<Book>();
        };

        public static Func<BookDTO, IBookRepository, Task<Book>> CheckUpdate = async (BookDTO, repo) =>
        {
            await GetName(BookDTO.Title, repo);
            await GetId(BookDTO.Id, repo);
            return BookDTO.Adapt<Book>();
        };

        public static Func<int, IBookRepository, Task<Book>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
