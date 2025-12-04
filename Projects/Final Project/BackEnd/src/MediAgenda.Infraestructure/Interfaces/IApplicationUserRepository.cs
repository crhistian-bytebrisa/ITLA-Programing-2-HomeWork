using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IApplicationUserRepository : IBaseRepositoryIdString<ApplicationUserModel>
    {
        Task<(List<ApplicationUserModel>, int)> GetAllAsync(ApplicationUserRequest request);
    }
}