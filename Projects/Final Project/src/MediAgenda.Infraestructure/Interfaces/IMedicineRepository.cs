using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IMedicineRepository : IBaseRepository<MedicineModel>
    {
        Task<(List<MedicineModel>, int)> GetByRequest(MedicineRequest request);
    }
}