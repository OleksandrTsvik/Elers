import {
  CoursePermissionType,
  PermissionType,
  useCoursePermission,
} from '../../auth';

interface Props {
  courseId: string | undefined;
  children: React.ReactNode;
}

export default function AssignmentSubmitGuard({ courseId, children }: Props) {
  const { isLoadingCoursePermission, isMember, checkCoursePermission } =
    useCoursePermission(courseId);

  if (isLoadingCoursePermission) {
    return null;
  }

  if (
    !isMember &&
    !checkCoursePermission(
      [
        CoursePermissionType.CreateCourseMaterial,
        CoursePermissionType.UpdateCourseMaterial,
        CoursePermissionType.DeleteCourseMaterial,
      ],
      [PermissionType.ManageCourse],
    )
  ) {
    return null;
  }

  return children;
}
