using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryWeb.Domain.Exceptions
{
    public class APIException : Exception
    {
        public APIException() : base()
        {

        }

        public APIException(string message) : base(message)
        {
            
        }
    }
}
