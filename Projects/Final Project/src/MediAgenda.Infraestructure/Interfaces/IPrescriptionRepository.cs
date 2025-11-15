using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPrescriptionRepository : IBaseRepository<PrescriptionModel>
    {
        Task<(List<PrescriptionModel>, int)> GetByRequest(PrescriptionRequest request);
    }
}