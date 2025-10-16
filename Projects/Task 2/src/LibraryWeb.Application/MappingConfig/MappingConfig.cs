using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Domain.Entities;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.MappingConfig
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<CreateBookDTO, Book>.NewConfig()
                .Map(dest => dest.BookGenres, src => src.Genres.Select(x => new BookGenre { GenreId = x }).ToList())
                .Map(dest => dest.BookLanguages, src => src.Languages.Select(x => new BookLanguage { LanguageId = x }).ToList());

            TypeAdapterConfig<BookDTO, Book>.NewConfig()
                .Map(dest => dest.BookGenres, source => source.Genres.Select(x => new BookGenre { Genre = x.Adapt<Genre>() }).ToList())
                .Map(dest => dest.BookLanguages, source => source.Languages.Select(x => new BookLanguage { Language = x.Adapt<Language>() }).ToList());

            TypeAdapterConfig<Book, BookDTO>.NewConfig()
                .Map(dest => dest.Genres, src => src.BookGenres.Select(bg => bg.Genre.Adapt<GenreDTO>()).ToList())
                .Map(dest => dest.Languages, src => src.BookLanguages.Select(bl => bl.Language.Adapt<LanguageDTO>()).ToList());

            TypeAdapterConfig<List<Book>, List<BookDTO>>.NewConfig()
                .Map(dest => dest, source => source.Select(book => book.Adapt<BookDTO>()).ToList());
        }
    }
}
