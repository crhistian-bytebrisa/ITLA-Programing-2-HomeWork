using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.EntityDTO
{
    public class GenreDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public GenreDTO(int id, string name, string description)
        {
            Id = id;
            Name = name;
            Description = description;
        }
    }
}
