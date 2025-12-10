using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IPermissionRepository : IBaseRepositoryIdInt<PermissionModel>
    {
        Task<(List<PermissionModel>, int)> GetAllAsync(PermissionRequest request);
    }
}