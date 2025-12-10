using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class MedicineException : Exception
    {
        public MedicineException()
        {
        }

        public MedicineException(string msg) : base(msg)
        {
        }
    }
}