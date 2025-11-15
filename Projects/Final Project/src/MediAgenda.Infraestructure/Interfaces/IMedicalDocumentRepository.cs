using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IMedicalDocumentRepository : IBaseRepository<MedicalDocumentModel>
    {
        Task<(List<MedicalDocumentModel>, int)> GetByRequest(MedicalDocumentRequest request);
    }
}