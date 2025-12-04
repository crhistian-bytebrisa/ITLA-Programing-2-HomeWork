using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IDayAvailableRepository : IBaseRepositoryIdInt<DayAvailableModel>
    {
        Task<(List<DayAvailableModel>, int)> GetAllAsync(DayAvailableRequest request);
    }
}