using MediAgenda.Infraestructure.Interfaces;

namespace MediAgenda.Infraestructure.Core
{
    public interface IBaseRepositoryIdInt<T> : IBaseRepository<T>  where T : class, IEntityInt
    {
        Task<T> GetByIdAsync(int id);
    }
}