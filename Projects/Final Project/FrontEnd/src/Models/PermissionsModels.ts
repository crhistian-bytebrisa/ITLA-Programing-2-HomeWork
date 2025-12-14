import { PrescriptionSimple } from "./PrescriptionModels";

export interface Permission {
  id: number;
  name: string;
  description: string;
  prescriptionsCount: number;
}

export interface PermissionSimple {
  id: number;
  name: string;
  description: string;
}

export interface PermissionCreate {
  name: string;
  description?: string;
}

export interface PermissionUpdate extends PermissionCreate {
  id: number;
}

export interface PrescriptionPermission {
  prescriptionId: number;
  prescription: PrescriptionSimple;
  permissionId: number;
  permission: PermissionSimple;
}

export interface PrescriptionPermissionSimple {
  prescriptionId: number;
  permissionId: number;
  permissionName: string;
}

export interface PrescriptionPermissionCreateUpdate {
  prescriptionId: number;
  permissionId: number;
}