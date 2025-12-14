import { ConsultationSimple } from "./ConsultationAndReasonsModels";
import { PatientSimple } from "./PatientsModels";

export interface NotePatient {
  id: number;
  patientId: number;
  patient: PatientSimple;
  title: string;
  content: string;
  createdAt: string;
  updateAt: string;
}

export interface NotePatientSimple {
  id: number;
  patientId: number;
  title: string;
  content: string;
  createdAt: string;
  updateAt: string;
}

export interface NotePatientCreate {
  patientId: number;
  title: string;
  content: string;
}

export interface NotePatientUpdate {
  id: number;
  title: string;
  content: string;
}

export interface NoteConsultation {
  id: number;
  consultationId: number;
  consultation: ConsultationSimple;
  title: string;
  content: string;
  createdAt: string;
  updateAt: string;
}

export interface NoteConsultationSimple {
  id: number;
  consultationId: number;
  title: string;
  content: string;
  createdAt: string;
  updateAt: string;
}

export interface NoteConsultationCreate {
  consultationId: number;
  title: string;
  content: string;
}

export interface NoteConsultationUpdate {
  id: number;
  title: string;
  content: string;
}