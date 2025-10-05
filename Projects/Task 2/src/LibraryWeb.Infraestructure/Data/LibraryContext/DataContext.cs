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

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Language> Languages { get; set; }
    }
}
