using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class PrescriptionException : Exception
    {
        public PrescriptionException()
        {
        }

        public PrescriptionException(string msg) : base(msg)
        {
        }
    }
}