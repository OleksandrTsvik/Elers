import { ItemType } from 'antd/es/menu/hooks/useItems';
import { ColumnGroupType, ColumnType } from 'antd/es/table';
import { Tab } from 'rc-tabs/lib/interface';

import { CoursePermissionType, PermissionType } from '../../auth';

export type GetActionItems<T = void> = (record: T) => ItemType[];

export type AuthItem = {
  show?: boolean;
  coursePermissions?: CoursePermissionType[];
  userPermissions?: PermissionType[];
};

export type AuthItemAction = ItemType & AuthItem;
export type AuthItemTab = Tab & AuthItem;

export type AuthItemColumn<RecordType> = (
  | ColumnGroupType<RecordType>
  | ColumnType<RecordType>
) & {
  coursePermissions: CoursePermissionType[];
  userPermissions: PermissionType[];
};
