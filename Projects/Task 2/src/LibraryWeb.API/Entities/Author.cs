using System.ComponentModel.DataAnnotations;

namespace LibraryWeb.API.Entities
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        
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
