using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities
{
    public class Analysis
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Analysis() { }
    }
}
