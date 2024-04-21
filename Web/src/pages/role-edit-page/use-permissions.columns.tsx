import { TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

interface DataType {
  id: string;
  name: string;
  description: string;
}

export default function usePermissionsColumns() {
  const { t } = useTranslation();

  const columns: TableColumnsType<DataType> = [
    {
      key: 'permission',
      dataIndex: 'name',
      title: t('role_edit_page.permission'),
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: t('role_edit_page.description'),
    },
  ];

  return columns;
}
