using Mapster;
using MediAgenda.Application.DTOs;
using MediAgenda.Application.DTOs.Enums;
using MediAgenda.Application.DTOs.Relations;
using MediAgenda.Domain.Entities;
using MediAgenda.Infraestructure.Models;
using MediAgenda.Infraestructure.Models.Enums;
using MediAgenda.Infraestructure.Models.Relations;

namespace MediAgenda.API.MaperConfig
{
    public class MappingConfig
    {
        public static void RegisterMappings()
        {
            //Analisis
            TypeAdapterConfig<AnalysisModel, AnalysisDTO>.NewConfig()
                .Map(dest => dest.PrescriptionAnalysesCount, src => src.PrescriptionAnalyses.Count);

            TypeAdapterConfig<AnalysisModel, AnalysisSimpleDTO>.NewConfig();
            TypeAdapterConfig<AnalysisCreateDTO, AnalysisModel>.NewConfig();
            TypeAdapterConfig<AnalysisUpdateDTO, AnalysisModel>.NewConfig();

            //Usuarios
            TypeAdapterConfig<ApplicationUserModel, ApplicationUserDTO>.NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Doctor, src => src.Doctor.Adapt<DoctorSimpleDTO>())
                .Map(dest => dest.Patient, src => src.Patient.Adapt<PatientSimpleDTO>());

            TypeAdapterConfig<ApplicationUserModel, ApplicationUserSimpleDTO>.NewConfig();
            TypeAdapterConfig<ApplicationUserCreateDTO, ApplicationUserModel>.NewConfig();

            //Clinicas
            TypeAdapterConfig<ClinicModel, ClinicDTO>.NewConfig()
                .Map(dest => dest.DaysAvailableCount, src => src.DaysAvailable.Count);

            TypeAdapterConfig<ClinicCreateDTO, ClinicModel>.NewConfig();
            TypeAdapterConfig<ClinicUpdateDTO, ClinicModel>.NewConfig();

            //Consultas
            TypeAdapterConfig<ConsultationModel, ConsultationDTO>.NewConfig()
                .Map(dest => dest.State, src => src.State.ToString())
                .Map(dest => dest.Patient, src => src.Patient.Adapt<PatientSimpleDTO>())
                .Map(dest => dest.Reason, src => src.Reason.Adapt<ReasonSimpleDTO>())
                .Map(dest => dest.DayAvailable, src => src.DayAvailable.Adapt<DayAvailableSimpleDTO>())
                .Map(dest => dest.NotesCount, src => src.Notes.Count)
                .Map(dest => dest.Prescription, src => src.Prescription.Adapt<PrescriptionSimpleDTO>());

            TypeAdapterConfig<ConsultationModel, ConsultationSimpleDTO>.NewConfig()
                .Map(dest => dest.State, src => src.State.ToString());

            TypeAdapterConfig<ConsultationCreateDTO, ConsultationModel>.NewConfig()
                .Map(dest => dest.State, src => src.State.Adapt<ConsultationState>());

            TypeAdapterConfig<ConsultationUpdateDTO, ConsultationModel>.NewConfig()
                .Map(dest => dest.State, src => src.State.Adapt<ConsultationState>());

            // Dias disponibles
            TypeAdapterConfig<DayAvailableModel, DayAvailableDTO>.NewConfig()
                .Map(dest => dest.Clinic, src => src.Clinic.Adapt<ClinicDTO>())
                .Map(dest => dest.Consultations, src => src.Consultations.Adapt<List<ConsultationSimpleDTO>>());

            TypeAdapterConfig<DayAvailableModel, DayAvailableSimpleDTO>.NewConfig()
                .Map(dest => dest.ConsultationsCount, src => src.Consultations.Where(x => x.State == ConsultationState.Pendent || x.State == ConsultationState.Confirmed).Count());

            TypeAdapterConfig<DayAvailableCreateDTO, DayAvailableModel>.NewConfig();
            TypeAdapterConfig<DayAvailableUpdateDTO, DayAvailableModel>.NewConfig();

            // Doctores
            TypeAdapterConfig<DoctorModel, DoctorDTO>.NewConfig()
                .Map(dest => dest.User, src => src.User.Adapt<ApplicationUserSimpleDTO>());

            TypeAdapterConfig<DoctorModel, DoctorSimpleDTO>.NewConfig();
            TypeAdapterConfig<DoctorCreateDTO, DoctorModel>.NewConfig();
            TypeAdapterConfig<DoctorUpdateDTO, DoctorModel>.NewConfig();

            // Seguros
            TypeAdapterConfig<InsuranceModel, InsuranceDTO>.NewConfig()
                .Map(dest => dest.PatientsCount, src => src.Patients.Count);

            TypeAdapterConfig<InsuranceModel, InsuranceSimpleDTO>.NewConfig();
            TypeAdapterConfig<InsuranceCreateDTO, InsuranceModel>.NewConfig();
            TypeAdapterConfig<InsuranceUpdateDTO, InsuranceModel>.NewConfig();

            // Documentos medicos
            TypeAdapterConfig<MedicalDocumentModel, MedicalDocumentDTO>.NewConfig()
                .Map(dest => dest.Patient, src => src.Patient.Adapt<PatientSimpleDTO>());

            TypeAdapterConfig<MedicalDocumentModel, MedicalDocumentSimpleDTO>.NewConfig();
            TypeAdapterConfig<MedicalDocumentCreateDTO, MedicalDocumentModel>.NewConfig();


            // Medicinas
            TypeAdapterConfig<MedicineModel, MedicineDTO>.NewConfig()
                .Map(dest => dest.PrescriptionMedicinesCount, src => src.PrescriptionMedicines.Count)
                .Map(dest => dest.CurrentMedicamentsCount, src => src.CurrentMedicaments.Count);

            TypeAdapterConfig<MedicineModel, MedicineSimpleDTO>.NewConfig();
            TypeAdapterConfig<MedicineCreateDTO, MedicineModel>.NewConfig();
            TypeAdapterConfig<MedicineUpdateDTO, MedicineModel>.NewConfig();

            // Notas de consulta
            TypeAdapterConfig<NoteConsultationModel, NoteConsultationDTO>.NewConfig()
                .Map(dest => dest.Consultation, src => src.Consultation.Adapt<ConsultationSimpleDTO>());

            TypeAdapterConfig<NoteConsultationModel, NoteConsultationSimpleDTO>.NewConfig();
            TypeAdapterConfig<NoteConsultationCreateDTO, NoteConsultationModel>.NewConfig();
            TypeAdapterConfig<NoteConsultationUpdateDTO, NoteConsultationModel>.NewConfig();

            // Notas de paciente
            TypeAdapterConfig<NotePatientModel, NotePatientDTO>.NewConfig()
                .Map(dest => dest.Patient, src => src.Patient.Adapt<PatientSimpleDTO>());

            TypeAdapterConfig<NotePatientModel, NotePatientSimpleDTO>.NewConfig();
            TypeAdapterConfig<NotePatientCreateDTO, NotePatientModel>.NewConfig();

            //Paciente
            TypeAdapterConfig<PatientModel, PatientDTO>.NewConfig()
                .Map(dest => dest.BloodType, src => src.Bloodtype.ToString())
                .Map(dest => dest.Gender, src => src.Gender.ToString())
                .Map(dest => dest.User, src => src.User.Adapt<ApplicationUserSimpleDTO>())
                .Map(dest => dest.Insurance, src => src.Insurance.Adapt<InsuranceSimpleDTO>())
                .Map(dest => dest.NotesCount, src => src.Notes != null? src.Notes.Count : 0)
                .Map(dest => dest.ConsultationsCount, src => src.Consultations != null? src.Consultations.Count : 0)
                .Map(dest => dest.MedicalDocumentsCount, src => src.MedicalDocuments != null? src.MedicalDocuments.Count : 0) 
                .Map(dest => dest.CurrentMedicamentsCount, src => src.CurrentMedicaments != null? src.CurrentMedicaments.Count : 0);

            TypeAdapterConfig<PatientModel, PatientSimpleDTO>.NewConfig()
                .Map(dest => dest.FullName, src => src.User.NameComplete)
                .Map(dest => dest.BloodType, src => src.Bloodtype.ToString())
                .Map(dest => dest.Gender, src => src.Gender.ToString());

            TypeAdapterConfig<PatientCreateDTO, PatientModel>.NewConfig()
                .Map(dest => dest.Bloodtype, src => src.BloodTypeDTO)
                .Map(dest => dest.Gender, src => src.GenderDTO);

            //Permisos
            TypeAdapterConfig<PermissionModel, PermissionDTO>.NewConfig()
                .Map(dest => dest.PrescriptionPermissions, src => src.PrescriptionPermissions.Adapt<List<PrescriptionPermissionDTO>>());

            TypeAdapterConfig<PermissionModel, PermissionSimpleDTO>.NewConfig();
            TypeAdapterConfig<PermissionCreateDTO, PermissionModel>.NewConfig();
            TypeAdapterConfig<PermissionUpdateDTO, PermissionModel>.NewConfig();

            //Prescripciones
            TypeAdapterConfig<PrescriptionModel, PrescriptionDTO>.NewConfig()
                .Map(dest => dest.Consultation, src => src.Consultation.Adapt<ConsultationSimpleDTO>())
                .Map(dest => dest.PermissionsCount, src => src.PrescriptionPermissions != null ? src.PrescriptionPermissions.Count : 0)
                .Map(dest => dest.MedicinesCount, src => src.PrescriptionMedicines != null ? src.PrescriptionMedicines.Count : 0)
                .Map(dest => dest.AnalysisCount, src => src.PrescriptionAnalysis != null ? src.PrescriptionAnalysis.Count : 0);

            TypeAdapterConfig<PrescriptionModel, PrescriptionSimpleDTO>.NewConfig();
            TypeAdapterConfig<PrescriptionCreateDTO, PrescriptionModel>.NewConfig();

            // Relacion Precripcion - Analisis
            TypeAdapterConfig<PrescriptionAnalysisModel, PrescriptionAnalysisDTO>.NewConfig()
                .Map(dest => dest.Prescription, src => src.Prescription.Adapt<PrescriptionSimpleDTO>())
                .Map(dest => dest.Analysis, src => src.Analysis.Adapt<AnalysisSimpleDTO>());

            TypeAdapterConfig<PrescriptionAnalysisCUDTO, PrescriptionAnalysisDTO>.NewConfig();

            TypeAdapterConfig<PrescriptionAnalysisModel, PrescriptionAnalysisSimpleDTO>.NewConfig()
                .Map(dest => dest.AnalysisName, src => src.Analysis.Name);

            // Relacion Precripcion - Medicinas
            TypeAdapterConfig<PrescriptionMedicineModel, PrescriptionMedicineDTO>.NewConfig()
                .Map(dest => dest.Prescription, src => src.Prescription.Adapt<PrescriptionSimpleDTO>())
                .Map(dest => dest.Medicine, src => src.Medicine.Adapt<MedicineSimpleDTO>());

            TypeAdapterConfig<PrescriptionMedicineCUDTO, PrescriptionMedicineDTO>.NewConfig();
            TypeAdapterConfig<PrescriptionMedicineDTO, PrescriptionMedicineDTO>.NewConfig();

            TypeAdapterConfig<PrescriptionMedicineModel, PrescriptionMedicineSimpleDTO>.NewConfig()
                .Map(dest => dest.MedicineName, src => src.Medicine.Name)
                .Map(dest => dest.MedicineFormat, src => src.Medicine.Format);

            // Relacion Precripcion - Permisos
            TypeAdapterConfig<PrescriptionPermissionModel, PrescriptionPermissionDTO>.NewConfig()
                .Map(dest => dest.Prescription, src => src.Prescription.Adapt<PrescriptionSimpleDTO>())
                .Map(dest => dest.Permission, src => src.Permission.Adapt<PermissionSimpleDTO>());

            TypeAdapterConfig<PrescriptionPermissionCUDTO, PrescriptionPermissionDTO>.NewConfig();

            TypeAdapterConfig<PrescriptionPermissionModel, PrescriptionPermissionSimpleDTO>.NewConfig()
                .Map(dest => dest.PermissionName, src => src.Permission.Name);

            // medicamentos actuales
            TypeAdapterConfig<HistoryMedicamentsModel, CurrentMedicamentDTO>.NewConfig()
                .Map(dest => dest.Patient, src => src.Patient.Adapt<PatientSimpleDTO>())
                .Map(dest => dest.Medicine, src => src.Medicine.Adapt<MedicineSimpleDTO>());

            TypeAdapterConfig<HistoryMedicamentsModel, CurrentMedicamentSimpleDTO>.NewConfig()
                .Map(dest => dest.MedicineName, src => src.Medicine.Name)
                .Map(dest => dest.Format, src => src.Medicine.Format);

            TypeAdapterConfig<CurrentMedicamentCreateDTO, HistoryMedicamentsModel>.NewConfig();

            TypeAdapterConfig<CurrentMedicamentCreateDTO, CurrentMedicamentDTO>.NewConfig();

            // Razones
            TypeAdapterConfig<ReasonModel, ReasonDTO>.NewConfig()
                .Map(dest => dest.ConsultationsCount, src => src.Consultations.Count);

            TypeAdapterConfig<ReasonModel, ReasonSimpleDTO>.NewConfig();
            TypeAdapterConfig<ReasonCreateDTO, ReasonModel>.NewConfig();
            TypeAdapterConfig<ReasonUpdateDTO, ReasonModel>.NewConfig();
        }
    }
}
