import { CourseTab } from './course-tab.interface';
import { CourseTabType } from '../shared';

export interface CourseListItem {
  id: string;
  title: string;
  description?: string;
  imageUrl?: string;

  countMembers: number;
  countMaterials: number;
  countAssignments: number;
  countTests: number;
}

export interface Course {
  id: string;
  title: string;
  description?: string;
  imageUrl?: string;
  tabType: CourseTabType;
  courseTabs: CourseTab[];
}

export interface CourseToEdit {
  id: string;
  title: string;
  description?: string;
  imageUrl?: string;
  tabType: CourseTabType;
  courseTabs: CourseTab[];
}

export interface MyCourseListItem {
  id: string;
  title: string;
  imageUrl?: string;
  isCreator: boolean;
}
