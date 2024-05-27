import { CourseTab } from './course-tab.interface';
import { CourseTabType } from '../shared';

export interface CourseListItem {
  id: string;
  title: string;
  description?: string;
  imageUrl?: string;
}

export interface Course {
  id: string;
  title: string;
  description?: string;
  imageUrl?: string;
  tabType: CourseTabType;
  courseTabs: CourseTab[];
}
