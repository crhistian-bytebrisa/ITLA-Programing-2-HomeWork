import { PatientSimple } from "./PatientsModels";

export interface MedicalDocument {
  id: number;
  patientId: number;
  patient: PatientSimple;
  fileName: string;
  fileUrl: string;
  documentType: string;
}

export interface MedicalDocumentSimple {
  id: number;
  patientId: number;
  fileName: string;
  fileUrl: string;
  documentType: string;
}

export interface MedicalDocumentCreate {
  file: File;
  patientId: number;
  fileName: string;
}