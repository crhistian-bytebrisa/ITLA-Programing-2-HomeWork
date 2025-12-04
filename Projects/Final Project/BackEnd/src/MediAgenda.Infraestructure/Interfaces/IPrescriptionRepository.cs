using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPrescriptionRepository : IBaseRepositoryIdInt<PrescriptionModel>
    {
        Task<(List<PrescriptionModel>, int)> GetAllAsync(PrescriptionRequest request);
    }
}