using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MediAgenda.Application.DTOs
{
    public class ReasonDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
        public int ConsultationsCount { get; set; }
    }

    public class ReasonSimpleDTO
    {
       
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
    }

    public class ReasonCreateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [MaxLength(100, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Description { get; set; }
        public bool Available { get; set; } = true;
    }

    public class ReasonUpdateDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]

        [MaxLength(100, ErrorMessage = "No puedes tener mas de {1} caracteres en {0}.")]
        public string Title { get; set; }

        [MaxLength(500,ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        public bool Available { get; set; }
    }

    public class ReasonPatchDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Available { get; set; }
    }
}
