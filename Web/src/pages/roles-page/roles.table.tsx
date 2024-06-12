import { Table } from 'antd';

import useRolesColumns from './use-roles.columns';
import { useGetListRolesQuery } from '../../api/roles.api';
import { TableContainer } from '../../components';
import usePagination from '../../hooks/use-pagination';

export default function RolesTable() {
  const { pagingParams, pagination } = usePagination({ pageSize: 10 });
  const columns = useRolesColumns();

  const { data, isFetching } = useGetListRolesQuery({ ...pagingParams });

  return (
    <TableContainer>
      <Table
        bordered
        loading={isFetching}
        columns={columns}
        dataSource={data?.items}
        rowKey={(record) => record.id}
        pagination={pagination(data?.pageSize, data?.totalCount)}
      />
    </TableContainer>
  );
}
