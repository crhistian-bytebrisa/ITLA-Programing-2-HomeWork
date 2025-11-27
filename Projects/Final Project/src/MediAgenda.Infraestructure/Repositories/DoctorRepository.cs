using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class DoctorRepository : BaseRepositoryIdInt<DoctorModel> , IDoctorRepository
    {
        public DoctorRepository(MediContext context) : base(context)
        {
        }
    }
}