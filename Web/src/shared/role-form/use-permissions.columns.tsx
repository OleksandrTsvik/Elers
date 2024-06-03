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
      key: 'description',
      dataIndex: 'description',
      title: t('roles_page.description'),
    },
  ];

  return columns;
}
