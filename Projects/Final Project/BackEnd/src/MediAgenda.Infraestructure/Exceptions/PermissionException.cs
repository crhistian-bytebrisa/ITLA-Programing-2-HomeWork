using System;

namespace MediAgenda.Infraestructure.Exceptions
{
    public class PermissionException : Exception
    {
        public PermissionException()
        {
        }

        public PermissionException(string msg) : base(msg)
        {
        }
    }
}