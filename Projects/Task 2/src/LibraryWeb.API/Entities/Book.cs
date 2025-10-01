using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryWeb.API.Entities
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public DateTime PublishedDate { get; set; }
        [Required]
        public int Pages { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;

        //Relationship with Author
        [Required]
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        //Navegation many to many with Genres
        public IEnumerable<Genre> Genres { get; set; } = new List<Genre>();
        public IEnumerable<Language> Languages { get; set; } = new List<Language>();

        public Book() { }

        public Book(int id, string title, Author author, DateTime publishedDate, int pages, bool isAvailable)
        {
            Id = id;
            Title = title;
            Author = author;
            PublishedDate = publishedDate;
            Pages = pages;
            IsAvailable = isAvailable;
        }
    }
}
