using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IClinicRepository : IBaseRepository<ClinicModel>
    {
        Task<(List<ClinicModel>, int)> GetByRequest(ClinicRequest request);
    }
}