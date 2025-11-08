using MediAgenda.Infraestructure.Context;
using MediAgenda.Infraestructure.Core;
using MediAgenda.Infraestructure.Interfaces;
using MediAgenda.Infraestructure.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MediAgenda.Infraestructure.Repositories
{
    public class ConsultationRepository : BaseRepository<ConsultationModel>, IConsultationRepository
    {
        public ConsultationRepository(MediContext context) : base(context)
        {
        }

    }
}