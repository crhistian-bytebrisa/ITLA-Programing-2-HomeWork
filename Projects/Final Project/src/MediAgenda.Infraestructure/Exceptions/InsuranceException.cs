using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class InsuranceException : Exception
    {
        public InsuranceException()
        {
        }

        public InsuranceException(string msg) : base(msg)
        {
        }
    }
}