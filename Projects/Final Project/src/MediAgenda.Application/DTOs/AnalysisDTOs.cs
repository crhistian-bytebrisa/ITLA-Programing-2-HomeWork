using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Domain.Core;

namespace MediAgenda.Application.DTOs
{
    public class AnalysisDTO: IHasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<PrescriptionAnalysisDTO> PrescriptionAnalyses { get; set; }
    }

    public class AnalysisSimpleDTO : IHasName
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class AnalysisCreateDTO : IHasName
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
