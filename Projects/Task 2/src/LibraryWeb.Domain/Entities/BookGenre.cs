using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Domain.Entities
{
    [Table("BooksGenres")]
    public class BookGenre
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }

        public BookGenre() { }

        public BookGenre(int bookId, int genreId)
        {
            BookId = bookId;
            GenreId = genreId;
        }

        public BookGenre(Book book, Genre genre)
        {
            Book = book;
            BookId = book.Id;
            Genre = genre;
            GenreId = genre.Id;
        }
    }
}
