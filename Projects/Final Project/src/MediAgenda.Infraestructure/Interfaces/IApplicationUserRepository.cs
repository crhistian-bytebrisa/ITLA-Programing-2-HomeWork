using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IApplicationUserRepository : IBaseRepository<ApplicationUserModel>
    {
        Task<(List<ApplicationUserModel>, int)> GetByRequest(ApplicationUserRequest request);
    }
}