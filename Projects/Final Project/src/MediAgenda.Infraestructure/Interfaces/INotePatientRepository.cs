using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface INotePatientRepository : IBaseRepository<NotePatientModel>
    {
        Task<(List<NotePatientModel>, int)> GetByRequest(NotePatientRequest request);
    }
}