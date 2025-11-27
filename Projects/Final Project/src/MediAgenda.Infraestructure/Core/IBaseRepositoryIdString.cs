using MediAgenda.Infraestructure.Interfaces;

namespace MediAgenda.Infraestructure.Core
{
    public interface IBaseRepositoryIdString<T> : IBaseRepository<T> where T : class, IEntityString
    {
        Task<T> GetByIdAsync(string id);
    }
}