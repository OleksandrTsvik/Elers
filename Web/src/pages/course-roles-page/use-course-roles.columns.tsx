import { TableColumnsType } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';

import { ActionsDropdown } from '../../components';
import { CourseRoleListItem } from '../../models/course-role.interface';

export default function useCourseRolesColumns(
  getActionItems: (record: CourseRoleListItem) => ItemType[],
): TableColumnsType<CourseRoleListItem> {
  const { t } = useTranslation();

  return [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'role',
      dataIndex: 'name',
      width: 1,
      title: t('course_roles_page.role'),
    },
    {
      key: 'numberPermissions',
      title: t('course_roles_page.number_permissions'),
      width: 1,
      render: (_, record) => record.coursePermissions.length,
    },
    {
      key: 'permissions',
      title: t('course_roles_page.permissions'),
      render: (_, record) =>
        record.coursePermissions.map((item) => item.description).join('; '),
    },
    {
      key: 'action',
      width: 1,
      render: (_, record) => <ActionsDropdown items={getActionItems(record)} />,
    },
  ];
}
