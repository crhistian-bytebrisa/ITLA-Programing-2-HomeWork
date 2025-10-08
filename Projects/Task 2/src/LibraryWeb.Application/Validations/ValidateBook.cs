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
            var book = await repo.GetByName(name);

            if (book != null)
            {
                throw new ApplicationException("Ya existe este lenguaje.");
            }
        }

        private static async Task<Book> GetId(int id, IBookRepository repo)
        {
            var book = await repo.GetByIdAsync(id);
            if (book == null)
            {
                throw new ApplicationException("No existe este lenguaje.");
            }

            return book;
        }

        public static Func<CreateBookDTO, IBookRepository, Task<Book>> CheckAdd = async (CbookDTO, repo) =>
        {
            await GetName(CbookDTO.Title, repo);
            return CbookDTO.Adapt<Book>();
        };

        public static Func<BookDTO, IBookRepository, Task<Book>> CheckUpdate = async (bookDTO, repo) =>
        {
            await GetName(bookDTO.Title, repo);
            await GetId(bookDTO.Id, repo);
            return bookDTO.Adapt<Book>();
        };

        public static Func<int, IBookRepository, Task<Book>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
