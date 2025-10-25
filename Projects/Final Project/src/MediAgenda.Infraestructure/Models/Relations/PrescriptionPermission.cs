using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models.Relations
{
    [Table("PrescriptionsPermisions")]
    public class PrescriptionPermission
    {
        [Required, ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public Prescription Prescription { get; set; }

        [Required, ForeignKey("Permission")]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
        public PrescriptionPermission() { }
    }
}
