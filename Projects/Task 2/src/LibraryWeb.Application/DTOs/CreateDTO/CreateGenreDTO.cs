using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.CreateDTO
{
    public class CreateGenreDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CreateGenreDTO(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
