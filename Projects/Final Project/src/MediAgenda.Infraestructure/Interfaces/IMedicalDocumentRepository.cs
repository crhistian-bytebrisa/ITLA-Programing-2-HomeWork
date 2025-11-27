using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IMedicalDocumentRepository : IBaseRepositoryIdInt<MedicalDocumentModel>
    {
        Task<(List<MedicalDocumentModel>, int)> GetAllAsync(MedicalDocumentRequest request);
    }
}