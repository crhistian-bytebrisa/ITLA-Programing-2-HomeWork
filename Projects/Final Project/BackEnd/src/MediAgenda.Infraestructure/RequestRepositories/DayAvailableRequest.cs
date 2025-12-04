using MediAgenda.Infraestructure.RequestRepositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class DayAvailableRequest : BaseRequest
    {
        public DateOnly? DateFrom { get; set; }
        public DateOnly? DateTo { get; set; }
        public int? ClinicId { get; set; }
        public bool? OnlyAvailable { get; set; }
        public TimeOnly? StartTimeFrom { get; set; }
        public TimeOnly? StartTimeTo { get; set; }
    }
}
