using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface INotePatientRepository : IBaseRepositoryIdInt<NotePatientModel>
    {
        Task<(List<NotePatientModel>, int)> GetAllAsync(NotePatientRequest request);
    }
}