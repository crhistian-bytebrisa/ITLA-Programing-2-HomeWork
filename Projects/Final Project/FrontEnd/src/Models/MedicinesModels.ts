import { PatientSimple } from "./PatientsModels";
import { PrescriptionSimple } from "./PrescriptionModels";

export interface Medicine {
  id: number;
  name: string;
  description: string;
  format: string;
  isActive: boolean;
  prescriptionMedicinesCount: number;
  currentMedicamentsCount: number;
}

export interface MedicineSimple {
  id: number;
  name: string;
  description: string;
  format: string;
}

export interface MedicineCreate {
  name: string;
  description?: string;
  format: string;
}

export interface MedicineUpdate {
  id: number;
  isActive: boolean;
}

export interface PrescriptionMedicine {
  prescriptionId: number;
  prescription: PrescriptionSimple;
  medicineId: number;
  medicine: MedicineSimple;
  startDosage: string;
  endDosage: string;
  instructions: string;
}

export interface PrescriptionMedicineSimple {
  prescriptionId: number;
  medicineId: number;
  medicineName: string;
  medicineFormat: string;
  startDosage: string;
  endDosage: string;
  instructions: string;
}

export interface PrescriptionMedicineCreateUpdate {
  prescriptionId: number;
  medicineId: number;
  instructions: string;
  startDosage: string;
  endDosage: string;
}

export interface CurrentMedicament {
  patientId: number;
  patient: PatientSimple;
  medicineId: number;
  medicine: MedicineSimple;
  startMedication: string;
  endMedication?: string;
  isActive: boolean;
}

export interface CurrentMedicamentSimple {
  patientId: number;
  medicineId: number;
  medicineName: string;
  format: string;
  startMedication: string;
  endMedication?: string;
  isActive: boolean;
}

export interface CurrentMedicamentCreate {
  patientId: number;
  medicineId: number;
  startMedication: string;
  endMedication?: string;
}