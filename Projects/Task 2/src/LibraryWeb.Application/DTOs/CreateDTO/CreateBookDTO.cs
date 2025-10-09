using LibraryWeb.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.CreateDTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int Pages { get; set; }
        public bool IsAvailable { get; set; } = true;
        public int AuthorId { get; set; }
        public List<int> Genres { get; set; }
        public List<int> Languages { get; set; }

        public CreateBookDTO(string title, DateTime publishedDate, int pages, bool isAvailable, int authorId, List<int> genres, List<int> languages)
        {
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
