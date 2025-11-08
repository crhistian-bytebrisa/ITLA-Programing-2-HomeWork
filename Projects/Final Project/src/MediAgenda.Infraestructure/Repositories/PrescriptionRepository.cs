using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Repositories
{
    public class PrescriptionRepository : BaseRepository<PrescriptionModel> 
    {
        public PrescriptionRepository(MediContext context) : base(context)
        {
        }
    }
}