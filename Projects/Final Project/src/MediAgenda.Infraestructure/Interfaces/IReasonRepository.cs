using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IReasonRepository : IBaseRepository<ReasonModel>
    {
        Task<(List<ReasonModel>, int)> GetByRequest(ReasonRequest request);
    }
}