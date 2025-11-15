using MediAgenda.API.DTOs.Relations;

namespace MediAgenda.API.DTOs
{
    public class AnalysisDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PrescriptionAnalysisDTO> PrescriptionAnalyses { get; set; }
    }

    public class AnalysisSimpleDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AnalysisCreateDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
