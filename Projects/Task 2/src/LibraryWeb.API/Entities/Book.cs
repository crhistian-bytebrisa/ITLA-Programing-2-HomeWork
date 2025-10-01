namespace LibraryWeb.API.Entities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public int Pages { get; set; }
        public bool IsAvailable { get; set; } = true;

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
