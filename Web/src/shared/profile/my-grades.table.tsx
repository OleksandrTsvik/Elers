import { Table } from 'antd';

import useMyGradesColumns from './use-my-grades.columns';
import { useGetCourseMyGradesQuery } from '../../api/grades.api';
import { NavigateToError } from '../../common/navigate';
import { TableContainer } from '../../components';

interface Props {
  courseId: string | undefined;
}

export function MyGradesTable({ courseId }: Props) {
  const { data, isFetching, error } = useGetCourseMyGradesQuery({ courseId });

  const columns = useMyGradesColumns(courseId);

  if (error) {
    return <NavigateToError error={error} />;
  }

  return (
    <TableContainer>
      <Table
        bordered
        loading={isFetching}
        columns={columns}
        dataSource={data}
        rowKey={(record) => record.assessment.id}
        pagination={false}
      />
    </TableContainer>
  );
}
