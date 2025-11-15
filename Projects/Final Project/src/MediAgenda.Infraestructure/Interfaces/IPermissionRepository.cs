using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPermissionRepository : IBaseRepository<PermissionModel>
    {
        Task<(List<PermissionModel>, int)> GetByRequest(PermissionRequest request);
    }
}