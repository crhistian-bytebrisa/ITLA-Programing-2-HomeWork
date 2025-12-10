using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class NotePatientException : Exception
    {
        public NotePatientException()
        {
        }

        public NotePatientException(string msg) : base(msg)
        {
        }
    }
}