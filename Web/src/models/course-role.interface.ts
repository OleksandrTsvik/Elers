export interface CourseRoleListItem {
  id: string;
  name: string;
  coursePermissions: { id: string; description: string }[];
}
