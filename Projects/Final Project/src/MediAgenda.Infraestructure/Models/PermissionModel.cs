using MediAgenda.Infraestructure.Models.Relations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediAgenda.Infraestructure.Models
{
    [Table("Permissions")]
    public class PermissionModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        //Navegation
        public List<PrescriptionPermissionModel> PrescriptionPermissions { get; set; }
        public PermissionModel() { }
    }
}
