import { ConsultationSimple } from "./ConsultationAndReasonsModels";

export interface Clinic {
  id: number;
  name: string;
  address: string;
  phoneNumber: string;
  daysAvailableCount: number;
}

export interface ClinicCreate {
  name: string;
  address: string;
  phoneNumber: string;
}

export interface ClinicUpdate extends ClinicCreate {
  id: number;
}

export interface DayAvailable {
  id: number;
  clinicId: number;
  clinic: Clinic;
  startTime: string;
  endTime: string;
  date: string;
  limit: number;
  consultations: ConsultationSimple[];
  availableSlots: number;
  isAvailable: boolean;
}

export interface DayAvailableSimple {
  id: number;
  clinicId: number;
  startTime: string;
  endTime: string;
  date: string;
  limit: number;
  consultationsCount: number;
  availableSlots: number;
  isAvailable: boolean;
}

export interface DayAvailableCreate {
  clinicId: number;
  startTime: string;
  endTime: string;
  date: string;
  limit: number;
}

export interface DayAvailableUpdate extends DayAvailableCreate {
  id: number;
}