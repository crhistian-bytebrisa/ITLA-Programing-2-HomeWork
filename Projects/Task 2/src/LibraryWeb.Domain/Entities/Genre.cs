using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Domain.Entities
{
    [Table("Genres")]
    [Index(nameof(Name), IsUnique = true)]
    public class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;

        //Navegation many to many with BookGenres
        public List<BookGenre> BooksGenres { get; set; } = new();

        public Genre() { }

        public Genre(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
