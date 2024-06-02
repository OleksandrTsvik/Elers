import { Table } from 'antd';

import { CourseRolesModalMode } from './course-roles.modal-mode.enum';
import useCourseRolesActions from './use-course-roles.actions';
import useCourseRolesColumns from './use-course-roles.columns';
import { TableContainer } from '../../components';
import { CourseRoleListItem } from '../../models/course-role.interface';

interface Props {
  isLoading: boolean;
  courseRoles?: CourseRoleListItem[];
  updateModalMode: (modalMode: CourseRolesModalMode) => void;
  updateCurrentCourseRole: (courseRole: CourseRoleListItem) => void;
}

export default function CourseRolesTable({
  isLoading,
  courseRoles,
  updateModalMode,
  updateCurrentCourseRole,
}: Props) {
  const { getActionItems } = useCourseRolesActions(
    updateModalMode,
    updateCurrentCourseRole,
  );

  const columns = useCourseRolesColumns(getActionItems);

  return (
    <TableContainer>
      <Table
        bordered
        loading={isLoading}
        columns={columns}
        dataSource={courseRoles}
        rowKey={(record) => record.id}
        pagination={false}
      />
    </TableContainer>
  );
}
