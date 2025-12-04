using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class ConsultationException : Exception
    {
        public ConsultationException()
        {

        }

        public ConsultationException(string msg) : base(msg)
        {

        }
    }
}
