using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Relations;

namespace MediAgenda.API.MaperConfig
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<AnalysisModel, AnalysisDTO>.NewConfig()
                .Map(dest => dest.PrescriptionAnalyses, src => src.PrescriptionAnalyses.Adapt<PrescriptionAnalysisDTO>());

            TypeAdapterConfig<ClinicModel, ClinicDTO>.NewConfig()
                .Map(dest => dest.DaysAvailable, src => src.DaysAvailable.Adapt<DayAvailableDTO>());

            TypeAdapterConfig<ApplicationUser, ApplicationUserDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.Id);

            TypeAdapterConfig<CurrentMedicamentsModel, CurrentMedicamentSimpleDTO>.NewConfig()
                .Map(dest => dest.MedicineName, src => src.Medicine.Name)
                .Map(dest => dest.Format, src => src.Medicine.Format);

        }
    }
}
