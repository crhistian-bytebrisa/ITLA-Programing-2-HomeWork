using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface INoteConsultationRepository : IBaseRepository<NoteConsultationModel>
    {
        Task<(List<NoteConsultationModel>, int)> GetByRequest(NoteConsultationRequest request);
    }
}