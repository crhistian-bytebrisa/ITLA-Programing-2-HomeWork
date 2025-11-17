using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Domain.Core;

namespace MediAgenda.Application.DTOs
{
    public class MedicineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
        public List<PrescriptionMedicineDTO> PrescriptionMedicines { get; set; }
        public List<CurrentMedicamentSimpleDTO> CurrentMedicaments { get; set; }
    }

    public class MedicineSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
    }

    public class MedicineCreateDTO : IHasName
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Format { get; set; }
    }
}
