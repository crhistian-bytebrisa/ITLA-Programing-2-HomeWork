namespace MediAgenda.Application.DTOs.Relations
{

    public class PrescriptionAnalysisDTO
    {
        public int PrescriptionId { get; set; }
        public PrescriptionSimpleDTO Prescription { get; set; }
        public int AnalysisId { get; set; }
        public AnalysisSimpleDTO Analysis { get; set; }
        public string Recomendations { get; set; }
    }

    public class PrescriptionAnalysisSimpleDTO
    {
        public int PrescriptionId { get; set; }
        public int AnalysisId { get; set; }
        public string AnalysisName { get; set; }
        public string Recomendations { get; set; }
    }

    public class PrescriptionAnalysisCreateDTO
    {
        public int PrescriptionId { get; set; }
        public int AnalysisId { get; set; }
        public string Recomendations { get; set; }
    }
}

