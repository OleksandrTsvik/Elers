import { TableColumnsType } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';

import { CoursePermissionType } from './course-permission-type.enum';
import { hasCoursePermission } from './has-course-permission.util';
import { PermissionType } from './permission-type.enum';
import { useAuth } from './use-auth';
import { useGetCourseMemberPermissionsQuery } from '../api/course-permissions.api';
import { AuthItemAction, AuthItemColumn } from '../common/types';

export function useCoursePermission(courseId: string | undefined) {
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

  const filterActions = (actions: AuthItemAction[]): ItemType[] =>
    actions
      .filter(
        (item) =>
          item.show?.() ??
          checkCoursePermission(item.coursePermissions, item.userPermissions),
      )
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      .map(({ coursePermissions, userPermissions, show, ...item }) => item);

  const filterColumns = <RecordType>(
    columns: AuthItemColumn<RecordType>[],
  ): TableColumnsType<RecordType> =>
    columns
      .filter(({ coursePermissions, userPermissions }) =>
        checkCoursePermission(coursePermissions, userPermissions),
      )
      // eslint-disable-next-line @typescript-eslint/no-unused-vars
      .map(({ coursePermissions, userPermissions, ...item }) => item);

  return {
    isCreator,
    isMember,
    memberPermissions,
    checkCoursePermission,
    filterActions,
    filterColumns,
    isLoadingCoursePermission: isFetching,
    error,
  };
}
