import { Table } from 'antd';

import useUsersColumns from './use-users.columns';
import { useGetListUsersQuery } from '../../api/users.api';
import { TableContainer } from '../../components';

export default function UsersTable() {
  const columns = useUsersColumns();

  const { data, isFetching } = useGetListUsersQuery();

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
