using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IInsuranceRepository : IBaseRepository<InsuranceModel>
    {
        Task<(List<InsuranceModel>, int)> GetByRequest(InsuranceRequest request);
    }
}