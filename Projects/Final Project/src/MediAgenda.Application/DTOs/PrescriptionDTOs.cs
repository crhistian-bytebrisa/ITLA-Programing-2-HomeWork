using MediAgenda.Application.DTOs.Relations;

namespace MediAgenda.Application.DTOs
{
    public class PrescriptionDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public ConsultationSimpleDTO Consultation { get; set; }
        public string GeneralRecomendations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastPrint { get; set; }
        public List<PrescriptionPermissionDTO> PrescriptionPermissions { get; set; }
        public List<PrescriptionMedicineDTO> PrescriptionMedicines { get; set; }
        public List<PrescriptionAnalysisDTO> PrescriptionAnalysis { get; set; }
    }

    public class PrescriptionSimpleDTO
    {
        public int Id { get; set; }
        public int ConsultationId { get; set; }
        public string GeneralRecomendations { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastPrint { get; set; }
    }

    public class PrescriptionCreateDTO
    {
        public int ConsultationId { get; set; }
        public string GeneralRecomendations { get; set; }
    }
}
