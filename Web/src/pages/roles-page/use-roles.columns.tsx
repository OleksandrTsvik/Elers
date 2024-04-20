import { blue, red } from '@ant-design/colors';
import { DeleteFilled, EditFilled, EllipsisOutlined } from '@ant-design/icons';
import { Button, Dropdown } from 'antd';
import { ItemType } from 'antd/es/menu/hooks/useItems';
import { ColumnsType } from 'antd/es/table';
import { useTranslation } from 'react-i18next';

import { ListRoleItem } from '../../models/role.interface';

export default function useRolesColumns() {
  const { t } = useTranslation();

  const columns: ColumnsType<ListRoleItem> = [
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
      key: 'age',
      dataIndex: 'permissionsCount',
      title: t('roles_page.permissions'),
    },
    {
      key: 'action',
      width: 1,
      render: () => (
        <Dropdown menu={{ items: actionItems }}>
          <Button type="text" icon={<EllipsisOutlined />} />
        </Dropdown>
      ),
    },
  ];

  const actionItems: ItemType[] = [
    {
      key: '1',
      icon: <EditFilled style={{ color: blue.primary }} />,
      label: t('actions.edit'),
    },
    {
      key: '2',
      icon: <DeleteFilled style={{ color: red.primary }} />,
      label: t('actions.delete'),
    },
  ];

  return columns;
}
