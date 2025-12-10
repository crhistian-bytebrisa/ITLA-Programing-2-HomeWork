using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface INoteConsultationRepository : IBaseRepositoryIdInt<NoteConsultationModel>
    {
        Task<(List<NoteConsultationModel>, int)> GetAllAsync(NoteConsultationRequest request);
    }
}