import { Table } from 'antd';

import useRolesColumns from './use-roles.columns';
import { useGetListRolesQuery } from '../../api/roles.api';
import { TableContainer } from '../../components';

export default function RolesTable() {
  const columns = useRolesColumns();

  const { data, isFetching } = useGetListRolesQuery();

  return (
    <TableContainer>
      <Table
        bordered
        loading={isFetching}
        columns={columns}
        dataSource={data}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
