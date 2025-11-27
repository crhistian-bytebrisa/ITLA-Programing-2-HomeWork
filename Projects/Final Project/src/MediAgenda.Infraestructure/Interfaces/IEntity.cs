using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Interfaces
{
    public interface IEntityInt
    {
        int Id { get; set; }
    }

    public interface IEntityString
    {
        string Id { get; set; }
    }
}
