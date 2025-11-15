using Mapster;
using MediAgenda.API.DTOs;
using MediAgenda.API.DTOs.Relations;
using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Models;

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

        }
    }
}
