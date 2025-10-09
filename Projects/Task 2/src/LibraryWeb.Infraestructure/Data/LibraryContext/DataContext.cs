using LibraryWeb.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Infraestructure.Data.LibraryContext
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BookGenre>()
                .HasKey(bg => new { bg.BookId, bg.GenreId });

            modelBuilder.Entity<BookLanguage>()
                .HasKey(bl => new { bl.BookId, bl.LanguageId });

        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }

        public DbSet<BookGenre> BooksGenres { get; set; }
        public DbSet<BookLanguage> BooksLanguages { get; set; }
    }
}
