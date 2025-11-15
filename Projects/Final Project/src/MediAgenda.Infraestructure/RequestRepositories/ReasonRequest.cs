using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class ReasonRequest : BaseRequest
    {
        public string? Title { get; set; }
        public bool? Available { get; set; }
        public bool? IncludeConsultations { get; set; }


    }
}
