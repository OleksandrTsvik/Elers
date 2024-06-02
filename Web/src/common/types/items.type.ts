import { ItemType } from 'antd/es/menu/hooks/useItems';
import { ColumnGroupType, ColumnType } from 'antd/es/table';

import { CoursePermissionType, PermissionType } from '../../auth';

export type GetActionItems<T = void> = (record: T) => ItemType[];

export type AuthItemAction = ItemType & {
  show?: () => boolean;
  coursePermissions: CoursePermissionType[];
  userPermissions: PermissionType[];
};

export type AuthItemColumn<RecordType> = (
  | ColumnGroupType<RecordType>
  | ColumnType<RecordType>
) & {
  coursePermissions: CoursePermissionType[];
  userPermissions: PermissionType[];
};
