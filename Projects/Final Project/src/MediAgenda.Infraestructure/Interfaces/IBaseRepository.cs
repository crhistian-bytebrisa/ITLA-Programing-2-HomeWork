namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        Task<T> UpdateAsync(T entity);
    }
}