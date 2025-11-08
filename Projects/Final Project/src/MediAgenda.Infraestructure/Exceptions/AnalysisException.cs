using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class AnalysisException : Exception
    {
        public AnalysisException()
        {
                
        }

        public AnalysisException(string msg) : base(msg)
        {
                
        }
    }
}
