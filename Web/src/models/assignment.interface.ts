import { UserDto } from './user.interface';

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
  teacher?: UserDto;
  status: SubmittedAssignmentStatus;
  attemptNumber: number;
  grade?: number;
  teacherComment?: string;
  text?: string;
  files: SubmitAssignmentFile[];
  submittedAt: Date;
}

export interface SubmittedAssignmentReview {
  submittedAssignmentId: string;
  assignmentId: string;
  title: string;
  description: string;
  deadline?: Date;
  maxFiles: number;
  maxGrade: number;
  student: UserDto;
  teacher?: UserDto;
  grade?: number;
  status: SubmittedAssignmentStatus;
  attemptNumber: number;
  teacherComment?: string;
  text?: string;
  files: SubmitAssignmentFile[];
  submittedAt: Date;
}

export enum SubmittedAssignmentStatus {
  Submitted = 'Submitted',
  Graded = 'Graded',
  Resubmit = 'Resubmit',
}

export interface SubmitAssignmentFile {
  fileName: string;
  uniqueFileName: string;
}
