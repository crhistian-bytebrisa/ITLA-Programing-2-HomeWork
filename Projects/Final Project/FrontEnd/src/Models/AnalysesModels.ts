import { PrescriptionSimple } from "./PrescriptionModels";

export interface Analysis {
  id: number;
  name: string;
  description: string;
  prescriptionAnalysesCount: number;
}

export interface AnalysisSimple {
  id: number;
  name: string;
  description: string;
}

export interface AnalysisCreate {
  name: string;
  description?: string;
}

export interface AnalysisUpdate {
  id: number;
  name: string;
  description?: string;
}

export interface PrescriptionAnalysis {
  prescriptionId: number;
  prescription: PrescriptionSimple;
  analysisId: number;
  analysis: AnalysisSimple;
  recomendations: string;
}

export interface PrescriptionAnalysisSimple {
  prescriptionId: number;
  analysisId: number;
  analysisName: string;
  recomendations: string;
}

export interface PrescriptionAnalysisCreateUpdate {
  prescriptionId: number;
  analysisId: number;
  recomendations: string;
}