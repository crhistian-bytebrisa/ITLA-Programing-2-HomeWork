using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPatientRepository : IBaseRepositoryIdInt<PatientModel>
    {
        Task<(List<PatientModel>, int)> GetAllAsync(PatientRequest request);
    }
}