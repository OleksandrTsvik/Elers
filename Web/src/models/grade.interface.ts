import { UserDto } from './user.interface';

export interface GetCourseGradesResponse {
  assessments: AssessmentItem[];
  studentGrades: CourseGradeItemResponse[];
}

export interface GetCourseMyGradeItemResponse {
  assessment: AssessmentItem;
  grade?: MyGradeResponse;
}

export type AssessmentItem = {
  id: string;
  title: string;
} & ConditionalAssessmentItem;

export type ConditionalAssessmentItem =
  | { type: GradeType.Test }
  | { type: GradeType.Assignment; maxGrade: number }
  | { type: GradeType.Manual; date: Date; maxGrade: number };

export interface CourseGradeItemResponse {
  student: UserDto;
  grades: GradeItemResponse[];
}

export type GradeItemResponse = {
  assessmentId: string;
  gradeId: string;
  grade?: number;
  createdAt: Date;
} & ConditionalGradeItemResponse;

export type MyGradeResponse = {
  assessmentId: string;
  grade?: number;
  createdAt: Date;
} & ConditionalGradeItemResponse;

export type ConditionalGradeItemResponse =
  | { type: GradeType.Test }
  | { type: GradeType.Assignment; teacher?: UserDto }
  | { type: GradeType.Manual; teacher?: UserDto };

export enum GradeType {
  Assignment = 'Assignment',
  Test = 'Test',
  Manual = 'Manual',
}
