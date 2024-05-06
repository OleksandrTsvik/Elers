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

export interface CourseTab {
  id: string;
  courseId: string;
  name: string;
  isActive: boolean;
  order: number;
  color?: string;
  showMaterialsCount: boolean;
}
