import { EllipsisOutlined } from '@ant-design/icons';
import { Button, Dropdown, TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import useRolesActions from './use-roles.actions';
import { ListRoleItem } from '../../models/role.interface';

export default function useRolesColumns() {
  const { t } = useTranslation();

  const { getActionItems } = useRolesActions();

  const columns: TableColumnsType<ListRoleItem> = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'role',
      dataIndex: 'name',
      title: t('roles_page.role'),
    },
    {
      key: 'numberPermissions',
      title: t('roles_page.number_permissions'),
      render: (_, record) => record.permissions.length,
    },
    {
      key: 'permissions',
      title: t('roles_page.permissions'),
      render: (_, record) => record.permissions.join(', '),
    },
    {
      key: 'action',
      width: 1,
      render: (_, record) => (
        <Dropdown menu={{ items: getActionItems(record) }}>
          <Button type="text" icon={<EllipsisOutlined />} />
        </Dropdown>
      ),
    },
  ];

  return columns;
}
