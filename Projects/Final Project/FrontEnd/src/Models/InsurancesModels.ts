export interface Insurance {
  id: number;
  name: string;
  patientsCount: number;
}

export interface InsuranceSimple {
  id: number;
  name: string;
}

export interface InsuranceCreate {
  name: string;
}

export interface InsuranceUpdate extends InsuranceCreate {
  id: number;
}