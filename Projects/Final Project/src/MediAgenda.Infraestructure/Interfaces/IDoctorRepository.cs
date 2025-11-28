using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.RequestRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IDoctorRepository : IBaseRepositoryIdInt<DoctorModel>
    {
        Task<(List<DoctorModel>, int)> GetAllAsync(DoctorRequest request);
    }
}
