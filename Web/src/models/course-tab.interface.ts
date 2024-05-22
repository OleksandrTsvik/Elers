import { CourseMaterial } from './course-material.type';

export interface CourseTab {
  id: string;
  courseId: string;
  name: string;
  isActive: boolean;
  order: number;
  color?: string;
  showMaterialsCount: boolean;
  materialCount: number;
  courseMaterials: CourseMaterial[];
}
