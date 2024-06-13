import { TableColumnsType } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { Tab } from 'rc-tabs/lib/interface';

import { CoursePermissionType } from './course-permission-type.enum';
import { hasCoursePermission } from './has-course-permission.util';
import { PermissionType } from './permission-type.enum';
import { useAuth } from './use-auth';
import { useGetCourseMemberPermissionsQuery } from '../api/course-permissions.api';
import {
  AuthCourseItem,
  AuthCourseItemAction,
  AuthCourseItemColumn,
  AuthCourseItemMenu,
  AuthCourseItemTab,
  MenuItem,
} from '../common/types';

export function useCoursePermission(courseId: string | undefined) {
  const { checkPermission } = useAuth();

  const { data, isFetching, error } = useGetCourseMemberPermissionsQuery({
    courseId,
  });

  const isCreator = data?.isCreator ?? false;
  const isMember = data?.isMember ?? false;
  const isStudent = data?.isStudent ?? false;
  const memberPermissions = data?.memberPermissions ?? [];

  const checkCoursePermission = (
    coursePermissions: CoursePermissionType | CoursePermissionType[],
    userPermissions: PermissionType | PermissionType[] = [],
  ): boolean =>
    isCreator ||
    hasCoursePermission(memberPermissions, coursePermissions) ||
    checkPermission(userPermissions);

  const filterItems = <T extends AuthCourseItem, U>(items: T[]): U[] =>
    items
      .filter(
        (item) =>
          item.show ??
          (item.check ||
            checkCoursePermission(
              item.coursePermissions ?? [],
              item.userPermissions,
            )),
      )
      .map(
        // eslint-disable-next-line @typescript-eslint/no-unused-vars
        ({ coursePermissions, userPermissions, show, check, ...item }) =>
          item as U,
      );

  const filterActions = (actions: AuthCourseItemAction[]): ItemType[] =>
    filterItems(actions);

  const filterTabs = (tabs: AuthCourseItemTab[]): Tab[] => filterItems(tabs);

  const filterMenu = (items: AuthCourseItemMenu[]): MenuItem[] =>
    filterItems(items);

  const filterColumns = <RecordType>(
    columns: AuthCourseItemColumn<RecordType>[],
  ): TableColumnsType<RecordType> => filterItems(columns);

  return {
    isCreator,
    isMember,
    isStudent,
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
