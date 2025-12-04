using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class MedicineRequest : BaseRequest
    {
        public string? Name { get; set; }
        public string? Format { get; set; }
        public bool? IncludePrescriptionsCount { get; set; } 
        public bool? IncludeCurrentMedicamentsCount { get; set; } 
    }
}
