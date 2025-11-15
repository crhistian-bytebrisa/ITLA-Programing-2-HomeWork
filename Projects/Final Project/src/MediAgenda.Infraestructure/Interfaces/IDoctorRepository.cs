using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<DoctorModel>
    {
    }
}
