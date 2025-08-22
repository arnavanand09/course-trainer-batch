// src/app/models/batch.ts

export interface Batch {
  batchId: number;
  batchName: string;
  startDate: string | Date;
  endDate: string | Date;
  isActive: boolean;
  courseId: number;
  trainerId?: number; // The '?' makes this property optional, matching the C# model's nullable 'int?'.
  createdBy: number;
  createdOn: Date;
  modifiedBy?: number;
  modifiedOn?: Date;
}