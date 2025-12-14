import { DayAvailableSimple } from "./ClinicsAndDaysModels";
import { ConsultationState } from "./Enums";
import { PatientSimple } from "./PatientsModels";
import { PrescriptionSimple } from "./PrescriptionModels";

export interface Reason {
  id: number;
  title: string;
  description: string;
  available: boolean;
  consultationsCount: number;
}

export interface ReasonSimple {
  id: number;
  title: string;
  description: string;
  available: boolean;
}

export interface ReasonCreate {
  title: string;
  description?: string;
  available?: boolean;
}

export interface ReasonUpdate {
  id: number;
  title: string;
  description?: string;
  available: boolean;
}

export interface Consultation {
  id: number;
  patientId: number;
  patient: PatientSimple;
  reasonId: number;
  reason: ReasonSimple;
  dayAvailableId: number;
  dayAvailable: DayAvailableSimple;
  state: string;
  turn: number;
  notesCount: number;
  prescription?: PrescriptionSimple;
}

export interface ConsultationSimple {
  id: number;
  patientId: number;
  reasonId: number;
  dayAvailableId: number;
  state: string;
  turn: number;
}

export interface ConsultationCreate {
  patientId: number;
  reasonId: number;
  dayAvailableId: number;
  state: ConsultationState;
}

export interface ConsultationUpdate extends ConsultationCreate {
  id: number;
}