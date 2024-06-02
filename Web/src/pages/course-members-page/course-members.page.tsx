import { useParams } from 'react-router-dom';

import CourseMembersBreadcrumb from './course-members.breadcrumb';
import CourseMembersHead from './course-members.head';
import CourseMembersTable from './course-members.table';

export default function CourseMembersPage() {
  const { courseId } = useParams();

  return (
    <>
      <CourseMembersHead />
      <CourseMembersBreadcrumb courseId={courseId} />
      <CourseMembersTable courseId={courseId} />
    </>
  );
}
