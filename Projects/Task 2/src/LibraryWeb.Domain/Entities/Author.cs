using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Domain.Entities
{
    [Table("Authors")]
    [Index(nameof(Name), IsUnique = true)]
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;

        //Navegation one to many with Book
        public IEnumerable<Book> Books { get; set; } = new List<Book>();
        public Author() { }

        public Author(int id, string name, string lastName)
        {
            Id = id;
            Name = name;
            LastName = lastName;
        }
    }
}
