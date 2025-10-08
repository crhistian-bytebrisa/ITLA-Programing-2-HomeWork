using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Domain.Exceptions
{
    public class InfraestructureException : Exception
    {
        public InfraestructureException() : base()
        {

        }

        public InfraestructureException(string message) : base(message)
        {

        }
    }
}
