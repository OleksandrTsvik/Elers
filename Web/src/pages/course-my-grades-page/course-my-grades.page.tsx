import { Table } from 'antd';
import { useParams } from 'react-router-dom';

import CourseMyGradesHead from './course-my-grades.head';
import useCourseMyGradesColumns from './use-course-my-grades.columns';
import { useGetCourseMyGradesQuery } from '../../api/grades.api';
import { NavigateToError } from '../../common/navigate';
import { TableContainer } from '../../components';

export default function CourseMyGradesPage() {
  const { courseId } = useParams();

  const { data, isFetching, error } = useGetCourseMyGradesQuery({ courseId });

  const columns = useCourseMyGradesColumns(courseId);

  if (error) {
    return <NavigateToError error={error} />;
  }

  return (
    <>
      <CourseMyGradesHead />
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
    </>
  );
}
