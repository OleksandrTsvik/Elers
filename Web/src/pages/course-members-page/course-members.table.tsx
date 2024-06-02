import { Table } from 'antd';

import useCourseMembersColumns from './use-course-members.columns';
import { useGetListCourseMembersQuery } from '../../api/course-members.queries.api';
import { useCoursePermission } from '../../auth';
import { NavigateToError } from '../../common/navigate';
import { TableContainer } from '../../components';
import usePagination from '../../hooks/use-pagination';
import useSortParams from '../../hooks/use-sort-params';
import useTableSearchProps from '../../hooks/use-table-search-props';
import { CourseMember } from '../../models/course-member.interface';

interface Props {
  courseId?: string;
}

export default function CourseMembersTable({ courseId }: Props) {
  const { pagingParams, pagination } = usePagination();
  const { filters, getColumnSearchProps } = useTableSearchProps<CourseMember>();
  const { sortParams, updateTableSortParams } = useSortParams();

  const { data, isFetching, error } = useGetListCourseMembersQuery({
    courseId,
    ...pagingParams,
    ...filters,
    ...sortParams,
  });

  const columns = useCourseMembersColumns(getColumnSearchProps, courseId);
  const { isLoadingCoursePermission } = useCoursePermission(courseId);

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
        onChange={updateTableSortParams}
      />
    </TableContainer>
  );
}
