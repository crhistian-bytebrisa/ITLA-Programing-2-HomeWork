using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPatientRepository : IBaseRepository<PatientModel>
    {
        Task<(List<PatientModel>, int)> GetByRequest(PatientRequest request);
    }
}