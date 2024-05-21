import { CourseTab } from './course-tab.interface';
import { CourseTabType } from '../shared';

export interface CourseListItem {
  id: string;
  title: string;
  description?: string;
  photoUrl?: string;
}

export interface Course {
  id: string;
  title: string;
  description?: string;
  photoUrl?: string;
  tabType: CourseTabType;
  courseTabs: CourseTab[];
}
