using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IDayAvailableRepository : IBaseRepository<DayAvailableModel>
    {
        Task<(List<DayAvailableModel>, int)> GetByRequest(DayAvailableRequest request);
    }
}