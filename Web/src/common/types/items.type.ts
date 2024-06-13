import { MenuProps } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { ColumnGroupType, ColumnType } from 'antd/es/table';
import { Tab } from 'rc-tabs/lib/interface';

import { CoursePermissionType, PermissionType } from '../../auth';

export type MenuItem = Required<MenuProps>['items'][number];

export type GetActionItems<T = void> = (record: T) => ItemType[];

export type AuthItem = {
  show?: boolean;
  check?: boolean;
  userPermissions?: PermissionType[];
};

export type AuthItemMenu = MenuItem & AuthItem;

export type AuthCourseItem = AuthItem & {
  coursePermissions?: CoursePermissionType[];
};

export type AuthCourseItemAction = ItemType & AuthCourseItem;
export type AuthCourseItemTab = Tab & AuthCourseItem;
export type AuthCourseItemMenu = MenuItem & AuthCourseItem;

export type AuthCourseItemColumn<RecordType> = (
  | ColumnGroupType<RecordType>
  | ColumnType<RecordType>
) &
  AuthCourseItem;
