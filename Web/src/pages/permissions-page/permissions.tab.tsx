import { Table, TableColumnsType } from 'antd';
import { useTranslation } from 'react-i18next';

import { useGetListPermissionsQuery } from '../../api/permissions.api';
import { TableContainer } from '../../components';
import { Permission } from '../../models/permission.interface';

export default function PermissionsTab() {
  const { t } = useTranslation();
  const { data, isFetching } = useGetListPermissionsQuery();

  const columns: TableColumnsType<Permission> = [
    {
      key: 'index',
      title: '#',
      width: 1,
      render: (_, __, index) => index + 1,
    },
    {
      key: 'description',
      dataIndex: 'description',
      title: t('permissions_page.description'),
    },
  ];

  return (
    <TableContainer>
      <Table
        loading={isFetching}
        columns={columns}
        dataSource={data}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
