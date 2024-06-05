export interface Assignment {
  assignmentId: string;
  courseTabId: string;
  title: string;
  description: string;
  deadline?: Date;
  maxFiles: number;
  maxGrade: number;
  submittedAssignment?: SubmittedAssignment;
}

export interface SubmittedAssignment {
  teacher?: {
    id: string;
    firstName: string;
    lastName: string;
    patronymic: string;
  };
  status: SubmittedAssignmentStatus;
  attemptNumber: number;
  grade?: number;
  teacherComment?: string;
  text?: string;
  files: { fileName: string; uniqueFileName: string }[];
  submittedAt: Date;
}

export enum SubmittedAssignmentStatus {
  Submitted = 'Submitted',
  Graded = 'Graded',
  Resubmit = 'Resubmit',
}
