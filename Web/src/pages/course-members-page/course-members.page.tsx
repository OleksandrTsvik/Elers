import { useParams } from 'react-router-dom';

import CourseMembersBreadcrumb from './course-members.breadcrumb';
import CourseMembersHead from './course-members.head';
import CourseMembersTable from './course-members.table';
import { useGetListCourseMembersQuery } from '../../api/course-members.queries.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function CourseMembersPage() {
  const { courseId } = useParams();

  const { data, isFetching, error } = useGetListCourseMembersQuery({
    courseId,
  });

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!isFetching && !data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <CourseMembersHead />
      <CourseMembersBreadcrumb courseId={courseId} />
      <CourseMembersTable
        courseId={courseId}
        isLoading={isFetching}
        courseMembers={data}
      />
    </>
  );
}
