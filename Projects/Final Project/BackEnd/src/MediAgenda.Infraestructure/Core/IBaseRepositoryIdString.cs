using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using Microsoft.AspNetCore.Identity;

namespace MediAgenda.Infraestructure.Core
{
    public interface IBaseRepositoryIdString<T> : IBaseRepository<T> where T : class, IEntityString
    {
        Task<T> GetByIdAsync(string id);
    }
}