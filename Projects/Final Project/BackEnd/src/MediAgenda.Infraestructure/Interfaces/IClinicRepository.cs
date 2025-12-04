using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IClinicRepository : IBaseRepositoryIdInt<ClinicModel>
    {
        Task<(List<ClinicModel>, int)> GetAllAsync(ClinicRequest request);
        Task<(List<DayAvailableModel>, int)> GetAllDaysAvailableById(int id, ClinicDaysAvailableRequest request);
    }
}