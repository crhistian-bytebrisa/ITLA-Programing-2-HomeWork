using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class ClinicException : Exception
    {
        public ClinicException()
        {

        }

        public ClinicException(string msg) : base(msg)
        {

        }
    }
}
