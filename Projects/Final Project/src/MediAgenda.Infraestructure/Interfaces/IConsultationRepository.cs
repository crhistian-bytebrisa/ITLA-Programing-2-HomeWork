using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IConsultationRepository : IBaseRepository<ConsultationModel>
    {
        Task<(List<ConsultationModel>, int)> GetByRequest(ConsultationRequest request);
    }
}