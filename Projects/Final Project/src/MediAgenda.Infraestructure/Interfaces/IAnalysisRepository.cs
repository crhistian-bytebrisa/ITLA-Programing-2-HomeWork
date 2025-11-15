using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IAnalysisRepository : IBaseRepository<AnalysisModel>
    {
        Task<(List<AnalysisModel>, int)> GetByRequest(AnalysisRequest request);
    }
}