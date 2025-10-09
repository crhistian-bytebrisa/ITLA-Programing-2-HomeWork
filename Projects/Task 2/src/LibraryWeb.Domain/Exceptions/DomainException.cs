using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Domain.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException() : base()
        {
            
        }

        public DomainException(string message) : base(message)
        {
            
        }
    }
}
