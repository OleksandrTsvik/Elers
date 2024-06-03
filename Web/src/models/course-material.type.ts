import { CourseMaterialType } from '../shared';

export type CourseMaterial = {
  id: string;
  courseTabId: string;
  isActive: boolean;
  order: number;
} & ConditionalCourseMaterial;

export type ConditionalCourseMaterial =
  | { type: CourseMaterialType.Content; content: string }
  | { type: CourseMaterialType.Link; title: string; link: string }
  | {
      type: CourseMaterialType.File;
      title: string;
      fileName: string;
      uniqueFileName: string;
    }
  | {
      type: CourseMaterialType.Assignment;
      title: string;
      description: string;
      deadline?: Date;
      maxFiles: number;
      maxGrade: number;
    };
