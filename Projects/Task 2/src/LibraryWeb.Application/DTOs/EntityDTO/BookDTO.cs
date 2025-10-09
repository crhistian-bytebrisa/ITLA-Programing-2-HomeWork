using LibraryWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.EntityDTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int Pages { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int AuthorId { get; set; }
        public List<LanguageDTO> Languages { get; set; }
        public List<GenreDTO> Genres { get; set; }

        public BookDTO(int id, string title, DateTime publishedDate, int pages, bool isAvailable, int authorId, List<GenreDTO> genres, List<LanguageDTO> languages)
        {
            Id = id;
            Title = title;
            PublishedDate = publishedDate;
            Pages = pages;
            IsAvailable = isAvailable;
            AuthorId = authorId;
            Genres = genres;
            Languages = languages;
        }
    }
}
