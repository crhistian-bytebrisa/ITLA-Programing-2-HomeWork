using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.CreateDTO
{
    public class CreateLanguageDTO
    {
        public string Name { get; set; }
        public CreateLanguageDTO(string name)
        {
            Name = name;
        }
    }
}
