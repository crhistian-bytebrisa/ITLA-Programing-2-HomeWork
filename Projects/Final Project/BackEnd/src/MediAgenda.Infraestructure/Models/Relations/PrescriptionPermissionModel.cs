using MediAgenda.Domain.Entities;
using Microsoft.EntityFrameworkCore;
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

    [PrimaryKey(nameof(PrescriptionId), nameof(PermissionId))]
    public class PrescriptionPermissionModel
    {
        [Required, ForeignKey("Prescription")]
        public int PrescriptionId { get; set; }
        public PrescriptionModel Prescription { get; set; }

        [Required, ForeignKey("Permission")]
        public int PermissionId { get; set; }
        public PermissionModel Permission { get; set; }
        public PrescriptionPermissionModel() { }
    }
}
