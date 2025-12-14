import { ApplicationUserSimple } from "./AuthModels";

export interface Doctor {
  id: number;
  userId: string;
  user: ApplicationUserSimple;
  specialty: string;
  aboutMe: string;
}

export interface DoctorSimple {
  id: number;
  userId: string;
  specialty: string;
  aboutMe: string;
}

export interface DoctorCreate {
  userId: string;
  specialty: string;
  aboutMe?: string;
}

export interface DoctorUpdate extends DoctorCreate {
  id: number;
}