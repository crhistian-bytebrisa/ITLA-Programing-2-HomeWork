using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IInsuranceRepository : IBaseRepositoryIdInt<InsuranceModel>
    {
        Task<(List<InsuranceModel>, int)> GetAllAsync(InsuranceRequest request);
    }
}