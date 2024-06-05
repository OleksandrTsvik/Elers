import { useParams } from 'react-router-dom';

import CourseMemberRoleModal from './course-member-role.modal';
import CourseMembersHead from './course-members.head';
import CourseMembersTable from './course-members.table';

export default function CourseMembersPage() {
  const { courseId } = useParams();

  return (
    <>
      <CourseMembersHead />
      <CourseMembersTable courseId={courseId} />
      <CourseMemberRoleModal courseId={courseId} />
    </>
  );
}
