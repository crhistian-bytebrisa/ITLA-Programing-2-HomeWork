using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class UserException : Exception
    {
        public UserException()
        {
        }

        public UserException(string msg) : base(msg)
        {
        }
    }
}