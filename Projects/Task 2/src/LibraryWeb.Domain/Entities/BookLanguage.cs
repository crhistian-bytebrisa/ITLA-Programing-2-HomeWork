using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Domain.Entities
{
    [Table("BooksLanguages")]
    public class BookLanguage
    {
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }

        public BookLanguage() { }

        public BookLanguage(int bookId, int languageId)
        {
            BookId = bookId;
            LanguageId = languageId;
        }

        public BookLanguage(Book book, Language language)
        {
            Book = book;
            BookId = book.Id;
            Language = language;
            LanguageId = language.Id;
        }
    }
}
