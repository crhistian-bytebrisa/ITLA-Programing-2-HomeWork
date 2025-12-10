using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class AnalysisRequest : BaseRequest
    {
        public string? Name { get; set; }
    }
    
}
