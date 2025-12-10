
namespace MediAgenda.Infraestructure.Core
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<(List<T>, int)> GetAllAsync();
        Task<T> UpdateAsync(T entity);
    }
}