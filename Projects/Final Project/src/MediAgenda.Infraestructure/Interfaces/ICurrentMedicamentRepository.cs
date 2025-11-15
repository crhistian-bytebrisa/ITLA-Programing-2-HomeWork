using MediAgenda.Infraestructure.Models.Relations;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface ICurrentMedicamentRepository : IBaseRepository<CurrentMedicamentsModel>
    {
        Task<List<CurrentMedicamentsModel>> GetByRequest(CurrentMedicamentRequest request);
    }
}