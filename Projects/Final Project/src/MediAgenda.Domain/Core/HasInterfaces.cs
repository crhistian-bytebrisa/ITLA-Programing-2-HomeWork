using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Core
{
    public interface IHasName
    {
        string Name { get; set; }
    }

    public interface IHasTitle
    {
        string Title { get; set; }
    }

    public interface IHasUsername
    {
        string UserName { get; set; }
    }
    
}
