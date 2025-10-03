using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.DTOs.EntityDTO
{
    public class LanguageDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public LanguageDTO(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
