using LibraryWeb.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.API.Data.LibraryContext
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Author> Authors { get; set; }
        DbSet<Book> Books { get; set; }
        DbSet<Genre> Genres { get; set; }
        DbSet<Language> Languages { get; set; }
    }
}
