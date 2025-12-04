using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class PatientException : Exception
    {
        public PatientException()
        {
        }

        public PatientException(string msg) : base(msg)
        {
        }
    }
}