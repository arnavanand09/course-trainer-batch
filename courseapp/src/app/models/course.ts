export interface Course {
  courseId: number;
  courseName: string;
  description: string;
  duration: number;
  createdBy: number;
  createdOn: string;   // backend sends string
  modifiedBy?: number;
  modifiedOn?: string;
  isActive: boolean;
}
