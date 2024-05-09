import { CourseTab } from './course-tab.interface';

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
  courseTabs: CourseTab[];
}
