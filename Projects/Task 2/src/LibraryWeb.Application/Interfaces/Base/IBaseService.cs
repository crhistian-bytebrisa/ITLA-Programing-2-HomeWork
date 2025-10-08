using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Application.Interfaces.Base
{
    public interface IBaseService<TDto, TCreateDto>
    {
        Task<List<TDto>> GetAllAsync();
        Task<TDto?> GetByIdAsync(int id);
        Task<TDto> AddAsync(TCreateDto dto);
        Task<TDto?> UpdateAsync(TDto dto);
        Task DeleteAsync(int id);
    }

}
