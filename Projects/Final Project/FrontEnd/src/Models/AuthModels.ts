import { BloodType, Gender } from "./Enums";

export interface ApplicationUser {
  id: string;
  email: string;
  nameComplete: string;
  phoneNumber: string;
}

export interface ApplicationUserSimple {
  id: string;
  email: string;
  nameComplete: string;
  phoneNumber: string;
}

export interface LoginDTO {
  email: string;
  password: string;
}

export interface RegisterDTO {
  email: string;
  password: string;
  nameComplete: string;
  phoneNumber: string;
  insuranceId: number;
  identification: string;
  dateOfBirth: string;
  bloodTypeDTO: BloodType;
  genderDTO: Gender;
}

export interface JWTResponse {
  user: ApplicationUser;
  expirationToken: string;
  token: string;
}