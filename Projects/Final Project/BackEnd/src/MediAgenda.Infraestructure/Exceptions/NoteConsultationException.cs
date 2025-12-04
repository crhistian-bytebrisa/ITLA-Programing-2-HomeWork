using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class NoteConsultationException : Exception
    {
        public NoteConsultationException()
        {
        }

        public NoteConsultationException(string msg) : base(msg)
        {
        }
    }
}