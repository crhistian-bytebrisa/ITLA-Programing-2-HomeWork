using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IConsultationRepository : IBaseRepositoryIdInt<ConsultationModel>
    {
        Task<(List<ConsultationModel>, int)> GetAllAsync(ConsultationRequest request);
    }
}