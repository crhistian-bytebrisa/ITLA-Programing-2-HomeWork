using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Domain.Entities.Relations
{
    public class PrescriptionPermission
    {
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public PrescriptionPermission() { }
    }
}
