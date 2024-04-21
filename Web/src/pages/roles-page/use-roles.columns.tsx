import { blue, red } from '@ant-design/colors';
import { DeleteFilled, EditFilled, EllipsisOutlined } from '@ant-design/icons';
import { Button, Dropdown, TableColumnsType } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { useTranslation } from 'react-i18next';
import { useNavigate } from 'react-router-dom';

import { ListRoleItem } from '../../models/role.interface';

export default function useRolesColumns() {
  const { t } = useTranslation();
  const navigate = useNavigate();

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

  const getActionItems = (record: ListRoleItem): ItemType[] => [
    {
      key: '1',
      icon: <EditFilled style={{ color: blue.primary }} />,
      label: t('actions.edit'),
      onClick: () => navigate(`/roles/edit/${record.id}`),
    },
    {
      key: '2',
      icon: <DeleteFilled style={{ color: red.primary }} />,
      label: t('actions.delete'),
    },
  ];

  return columns;
}
