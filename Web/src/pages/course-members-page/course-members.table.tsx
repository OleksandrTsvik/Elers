import { Table } from 'antd';

import useCourseMembersColumns from './use-course-members.columns';
import { useCoursePermission } from '../../auth';
import { TableContainer } from '../../components';
import { CourseMember } from '../../models/course-member.interface';

interface Props {
  courseId?: string;
  isLoading: boolean;
  courseMembers?: CourseMember[];
}

export default function CourseMembersTable({
  courseId,
  isLoading,
  courseMembers,
}: Props) {
  const columns = useCourseMembersColumns(courseId);

  const { isLoadingCoursePermission } = useCoursePermission(courseId);

  return (
    <TableContainer>
      <Table
        bordered
        loading={isLoading || isLoadingCoursePermission}
        columns={columns}
        dataSource={courseMembers}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
