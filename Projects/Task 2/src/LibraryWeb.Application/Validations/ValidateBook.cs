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
        private static async Task GetNameUpdate(int id, string name, IBookRepository repo)
        {
            var book = await repo.GetByName(name);

            if (book != null && book.Id != id)
            {
                throw new ApplicationException("Ya existe este Libro.");
            }
        }
        private static async Task GetName(string name, IBookRepository repo)
        {
            var book = await repo.GetByName(name);

            if (book != null )
            {
                throw new ApplicationException("Ya existe este Libro.");
            }
        }

        private static async Task<Book> GetId(int id, IBookRepository repo)
        {
            var book = await repo.GetByIdAsync(id);
            if (book == null)
            {
                throw new ApplicationException("No existe este Libro.");
            }

            return book;
        }

        public static Func<CreateBookDTO, IBookRepository, Task<Book>> CheckAdd = async (CbookDTO, repo) =>
        {
            await GetName(CbookDTO.Title, repo);
            return CbookDTO.Adapt<Book>();
        };

        public static Func<int,CreateBookDTO, IBookRepository, Task<Book>> CheckUpdate = async (id, bookDTO, repo) =>
        {
            await GetNameUpdate(id,bookDTO.Title, repo);
            var b = await GetId(id, repo);
            b.Adapt(bookDTO);
            b.BookGenres = bookDTO.Genres.Select(x=> new BookGenre { GenreId = x }).ToList();
            b.BookLanguages = bookDTO.Languages.Select(x => new BookLanguage { LanguageId = x }).ToList();
            return b;
        };

        public static Func<int, IBookRepository, Task<Book>> CheckDelete = async (id, repo) =>
        {
            return await GetId(id, repo);
        };
    }
}
