import { Table } from 'antd';

import usePermissionsColumns from './use-permissions.columns';
import { useGetListPermissionsQuery } from '../../api/permissions.api';
import { TableContainer } from '../../components';

export default function PermissionsTable() {
  const columns = usePermissionsColumns();

  const { data, isFetching } = useGetListPermissionsQuery();

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
