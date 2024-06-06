import { TableColumnsType } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { Tab } from 'rc-tabs/lib/interface';

import { CoursePermissionType } from './course-permission-type.enum';
import { hasCoursePermission } from './has-course-permission.util';
import { PermissionType } from './permission-type.enum';
import { useAuth } from './use-auth';
import { useGetCourseMemberPermissionsQuery } from '../api/course-permissions.api';
import {
  AuthItem,
  AuthItemAction,
  AuthItemColumn,
  AuthItemMenu,
  AuthItemTab,
  MenuItem,
} from '../common/types';

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

  const filterItems = <T extends AuthItem, U>(items: T[]): U[] =>
    items
      .filter(
        (item) =>
          item.show ??
          checkCoursePermission(
            item.coursePermissions ?? [],
            item.userPermissions,
          ),
      )
      .map(
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        ({ coursePermissions, userPermissions, show, ...item }) => item as U,
      );

  const filterActions = (actions: AuthItemAction[]): ItemType[] =>
    filterItems(actions);

  const filterTabs = (tabs: AuthItemTab[]): Tab[] => filterItems(tabs);

  const filterMenu = (tabs: AuthItemMenu[]): MenuItem[] => filterItems(tabs);

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
    filterMenu,
    filterTabs,
    filterColumns,
    isLoadingCoursePermission: isFetching,
    error,
  };
}
