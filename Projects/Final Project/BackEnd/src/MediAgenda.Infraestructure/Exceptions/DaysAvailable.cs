using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class DaysAvailableException : Exception
    {
        public DaysAvailableException()
        {

        }

        public DaysAvailableException(string msg) : base(msg)
        {

        }
    }
}
