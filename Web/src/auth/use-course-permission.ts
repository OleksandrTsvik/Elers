import { CoursePermissionType } from './course-permission-type.enum';
import { hasCoursePermission } from './has-course-permission.util';
import { PermissionType } from './permission-type.enum';
import { useAuth } from './use-auth';
import { useGetCourseMemberPermissionsQuery } from '../api/course-permissions.api';

export function useCoursePermission(courseId: string) {
  const { checkPermission } = useAuth();

  const { data, isFetching, error } = useGetCourseMemberPermissionsQuery({
    courseId,
  });

  const isCreator = data?.isCreator ?? false;
  const isMember = data?.isMember ?? false;
  const memberPermissions = data?.memberPermissions ?? [];

  const checkCoursePermission = (
    coursePermissions: CoursePermissionType | CoursePermissionType[],
    userPermissions: PermissionType | PermissionType[] = [],
  ): boolean =>
    isCreator ||
    hasCoursePermission(memberPermissions, coursePermissions) ||
    checkPermission(userPermissions);

  return {
    isCreator,
    isMember,
    memberPermissions,
    checkCoursePermission,
    isLoading: isFetching,
    error,
  };
}
