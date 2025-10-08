using LibraryWeb.Application.DTOs.CreateDTO;
using LibraryWeb.Application.DTOs.EntityDTO;
using LibraryWeb.Application.Interfaces.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.Interfaces
{
    public interface IBookService : IBaseService<BookDTO, CreateBookDTO>
    {
        Task<List<BookDTO>> GetAllWithDetailsAsync();
        Task<BookDTO> GetWithDetailsByIdAsync(int id);
    }

}
