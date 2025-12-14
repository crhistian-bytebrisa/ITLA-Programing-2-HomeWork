using MediAgenda.Application.DTOs.Relations;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class PermissionDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PrescriptionsCount { get; set; }
    }

    public class PermissionSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PermissionCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(500, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PermissionUpdateDTO : PermissionCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }
    }
}
