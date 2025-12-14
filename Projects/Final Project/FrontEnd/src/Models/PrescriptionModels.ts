import { ConsultationSimple } from "./ConsultationAndReasonsModels";

export interface Prescription {
  id: number;
  consultationId: number;
  consultation: ConsultationSimple;
  generalRecomendations: string;
  createdAt: string;
  lastPrint: string;
  analysisCount: number;
  medicinesCount: number;
  permissionsCount: number;
}

export interface PrescriptionSimple {
  id: number;
  consultationId: number;
  generalRecomendations: string;
  createdAt: string;
  lastPrint: string;
}

export interface PrescriptionCreate {
  consultationId: number;
  generalRecomendations: string;
}