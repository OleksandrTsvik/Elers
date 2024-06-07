import dayjs from 'dayjs';

import {
  CoursePermissionType,
  PermissionType,
  useCoursePermission,
} from '../../auth';
import { SubmittedAssignmentStatus } from '../../models/assignment.interface';

interface Props {
  courseId: string | undefined;
  deadline: Date | undefined;
  status: SubmittedAssignmentStatus | undefined;
  children: React.ReactNode;
}

export default function AssignmentSubmitGuard({
  courseId,
  deadline,
  status,
  children,
}: Props) {
  const { isLoadingCoursePermission, isMember, checkCoursePermission } =
    useCoursePermission(courseId);

  if (
    status !== SubmittedAssignmentStatus.Submitted &&
    status !== SubmittedAssignmentStatus.Resubmit &&
    deadline &&
    dayjs(deadline).isBefore(new Date(), 'date') &&
    !dayjs(deadline).isSame(new Date(), 'date')
  ) {
    return null;
  }

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
