using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IReasonRepository : IBaseRepositoryIdInt<ReasonModel>
    {
        Task<(List<ReasonModel>, int)> GetAllAsync(ReasonRequest request);
    }
}