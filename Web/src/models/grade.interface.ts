import { UserDto } from './user.interface';

export interface GetCourseGradesResponse {
  assessments: AssessmentItem[];
  studentGrades: CourseGradeItemResponse[];
}

export interface GetCourseMyGradeItemResponse {
  assessment: AssessmentItem;
  grade?: MyGradeResponse;
}

export interface AssessmentItem {
  id: string;
  type: GradeType;
  title: string;
}

export interface CourseGradeItemResponse {
  student: UserDto;
  grades: GradeItemResponse[];
}

export type GradeItemResponse = {
  assessmentId: string;
  gradeId: string;
  grade: number;
  createdAt: Date;
} & ConditionalGradeItemResponse;

export type MyGradeResponse = {
  assessmentId: string;
  grade: number;
  createdAt: Date;
} & ConditionalGradeItemResponse;

export type ConditionalGradeItemResponse =
  | { type: GradeType.Test }
  | { type: GradeType.Assignment; teacher?: UserDto };

export enum GradeType {
  Assignment = 'Assignment',
  Test = 'Test',
}
