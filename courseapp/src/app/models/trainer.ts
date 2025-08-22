// src/app/models/trainer.model.ts
export interface Trainer {
  trainerId: number;
  trainerName: string;
  expertise?: string;
  email?: string;
  phone?: string;
  isActive: boolean;

  // Audit fields
  createdBy: number;
  createdOn: string;
  modifiedBy?: number;
  modifiedOn?: string;

  // Navigation property (not used directly in UI but kept for sync)
  batches?: any[];
}
