using LibraryWeb.Domain.Entities;
using LibraryWeb.Domain.Interfaces.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Domain.Interfaces.Repositories
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        public Task<List<Book>> GetAllWithDetailsAsync();

        public Task<Book> GetWithDetailsByIdAsync(int id);
    }
}
