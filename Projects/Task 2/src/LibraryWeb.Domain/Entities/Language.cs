using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace LibraryWeb.Domain.Entities
{
    [Table("Languages")]
    [Index(nameof(Name), IsUnique = true)]
    public class Language
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;

        //Navegation many to many with BookLanguage
        public List<BookLanguage> BooksLanguages { get; set; } = new ();
        public Language() { }

        public Language(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
