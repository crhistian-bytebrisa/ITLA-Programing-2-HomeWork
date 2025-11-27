using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class ClinicRequest : BaseRequest
    {
        public string? Name { get; set; }
    }

    public class ClinicDaysAvailableRequest : BaseRequest
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public bool? OnlyAvailable { get; set; }
        public TimeOnly? StartTimeFrom { get; set; }
        public TimeOnly? StartTimeTo { get; set; }
    }
}
