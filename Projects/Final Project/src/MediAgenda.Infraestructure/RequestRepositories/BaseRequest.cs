using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.RequestRepositories
{
    public class BaseRequest
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
