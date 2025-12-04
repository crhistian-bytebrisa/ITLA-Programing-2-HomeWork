using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class DoctorException : Exception
    {
        public DoctorException()
        {

        }

        public DoctorException(string msg) : base(msg)
        {

        }
    }
}
