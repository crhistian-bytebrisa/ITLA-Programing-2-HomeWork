using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.CreateDTO
{
    public class CreateAuthorDTO
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public CreateAuthorDTO(string name, string lastName)
        {
            Name = name;
            LastName = lastName;
        }
    }
}
