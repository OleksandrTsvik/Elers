import { Table, TableProps } from 'antd';
import { useState } from 'react';

import useUsersColumns from './use-users.columns';
import { useGetListUsersQuery } from '../../api/users.api';
import { TableContainer } from '../../components';
import usePagination from '../../hooks/use-pagination';
import useSortParams from '../../hooks/use-sort-params';
import useTableSearchProps from '../../hooks/use-table-search-props';
import { User } from '../../models/user.interface';
import { isArrayOfStrings } from '../../utils/helpers';

export default function UsersTable() {
  const { pagingParams, pagination } = usePagination();
  const { filters, getColumnSearchProps } = useTableSearchProps<User>();
  const { sortParams, updateTableSortParams } = useSortParams();
  const [roles, setRoles] = useState<string[]>();
  const [types, setTypes] = useState<string[]>();

  const { data, isFetching } = useGetListUsersQuery({
    ...pagingParams,
    ...filters,
    ...sortParams,
    roles,
    types,
  });

  const columns = useUsersColumns(getColumnSearchProps);

  const handleChangeTableFilters: TableProps['onChange'] = (
    pagination,
    filters,
    sorter,
    extra,
  ) => {
    updateTableSortParams(pagination, filters, sorter, extra);

    if (isArrayOfStrings(filters['roles'])) {
      setRoles(filters['roles']);
    } else {
      setRoles(undefined);
    }

    if (isArrayOfStrings(filters['type'])) {
      setTypes(filters['type']);
    } else {
      setTypes(undefined);
    }
  };

  return (
    <TableContainer>
      <Table
        bordered
        loading={isFetching}
        columns={columns}
        dataSource={data?.items}
        rowKey={(record) => record.id}
        pagination={pagination(data?.pageSize, data?.totalCount)}
        onChange={handleChangeTableFilters}
      />
    </TableContainer>
  );
}
