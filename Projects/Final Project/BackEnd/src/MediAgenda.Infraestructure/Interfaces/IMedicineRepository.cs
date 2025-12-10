using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IMedicineRepository : IBaseRepositoryIdInt<MedicineModel>
    {
        Task<(List<MedicineModel>, int)> GetAllAsync(MedicineRequest request);
    }
}