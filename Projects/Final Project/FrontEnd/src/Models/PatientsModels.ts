import { ApplicationUserSimple } from "./AuthModels";
import { BloodType, Gender } from "./Enums";
import { InsuranceSimple } from "./InsurancesModels";

export interface Patient {
  id: number;
  userId: string;
  user: ApplicationUserSimple;
  insuranceId: number;
  insurance: InsuranceSimple;
  identification: string;
  dateOfBirth: string;
  age: number;
  bloodType: string;
  gender: string;
  notesCount: number;
  consultationsCount: number;
  medicalDocumentsCount: number;
  currentMedicamentsCount: number;
}

export interface PatientSimple {
  id: number;
  userId: string;
  fullName: string;
  insuranceId: number;
  identification: string;
  dateOfBirth: string;
  age: number;
  bloodType: string;
  gender: string;
}

export interface PatientCreate {
  userId: string;
  insuranceId: number;
  identification: string;
  dateOfBirth: string;
  bloodTypeDTO: BloodType;
  genderDTO: Gender;
}

export interface PatientUpdate {
  id: number;
  insuranceId: number;
  identification: string;
  dateOfBirth: string;
  bloodTypeDTO: BloodType;
  genderDTO: Gender;
}