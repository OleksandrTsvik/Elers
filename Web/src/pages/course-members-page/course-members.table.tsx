import { Table, TableProps } from 'antd';
import { useState } from 'react';

import useCourseMembersColumns from './use-course-members.columns';
import { useGetListCourseMembersQuery } from '../../api/course-members.queries.api';
import { useCoursePermission } from '../../auth';
import { NavigateToError } from '../../common/navigate';
import { TableContainer } from '../../components';
import usePagination from '../../hooks/use-pagination';
import useSortParams from '../../hooks/use-sort-params';
import useTableSearchProps from '../../hooks/use-table-search-props';
import { CourseMember } from '../../models/course-member.interface';
import { isArrayOfStrings } from '../../utils/helpers';

interface Props {
  courseId?: string;
}

export default function CourseMembersTable({ courseId }: Props) {
  const { pagingParams, pagination } = usePagination();
  const { filters, getColumnSearchProps } = useTableSearchProps<CourseMember>();
  const { sortParams, updateTableSortParams } = useSortParams();
  const [roles, setRoles] = useState<string[]>();

  const { data, isFetching, error } = useGetListCourseMembersQuery({
    courseId,
    ...pagingParams,
    ...filters,
    ...sortParams,
    roles,
  });

  const columns = useCourseMembersColumns(getColumnSearchProps, courseId);
  const { isLoadingCoursePermission } = useCoursePermission(courseId);

  const handleChangeTableFilters: TableProps['onChange'] = (
    pagination,
    filters,
    sorter,
    extra,
  ) => {
    updateTableSortParams(pagination, filters, sorter, extra);

    if (isArrayOfStrings(filters['role'])) {
      setRoles(filters['role']);
    } else {
      setRoles(undefined);
    }
  };

  if (error) {
    return <NavigateToError error={error} />;
  }

  return (
    <TableContainer>
      <Table
        bordered
        loading={isFetching || isLoadingCoursePermission}
        columns={columns}
        dataSource={data?.items}
        rowKey={(record) => record.id}
        pagination={pagination(data?.pageSize, data?.totalCount)}
        onChange={handleChangeTableFilters}
      />
    </TableContainer>
  );
}
