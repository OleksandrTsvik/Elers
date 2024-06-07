import { CoursePermissionType } from '../auth';

export interface CourseMemberPermissions {
  isCreator: boolean;
  isMember: boolean;
  isStudent: boolean;
  memberPermissions: CoursePermissionType[];
}

export interface CoursePermissionListItem {
  id: string;
  description: string;
}
