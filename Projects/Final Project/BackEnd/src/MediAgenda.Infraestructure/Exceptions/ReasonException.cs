using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class ReasonException : Exception
    {
        public ReasonException()
        {
        }

        public ReasonException(string msg) : base(msg)
        {
        }
    }
}