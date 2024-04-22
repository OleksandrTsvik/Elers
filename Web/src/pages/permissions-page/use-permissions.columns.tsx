import { TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import { Permission } from '../../models/permission.interface';

export default function usePermissionsColumns() {
  const { t } = useTranslation();

  const columns: TableColumnsType<Permission> = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'permission',
      dataIndex: 'name',
      title: t('permissions_page.permission'),
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: t('permissions_page.description'),
    },
  ];

  return columns;
}
