import { useState } from 'react';
import { useParams } from 'react-router-dom';

import CourseRoleCreationButton from './course-role-creation.button';
import CourseRolesBreadcrumb from './course-roles.breadcrumb';
import CourseRolesHead from './course-roles.head';
import { CourseRolesModalMode } from './course-roles.modal-mode.enum';
import CourseRolesModals from './course-roles.modals';
import CourseRolesTable from './course-roles.table';
import { useGetListCoursePermissionsQuery } from '../../api/course-permissions.api';
import { useGetListCourseRolesQuery } from '../../api/course-roles.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';
import { CourseRoleListItem } from '../../models/course-role.interface';

export default function CourseRolesPage() {
  const { courseId } = useParams();

  const [modalMode, setModalMode] = useState<CourseRolesModalMode>();
  const [currentCourseRole, setCurrentCourseRole] =
    useState<CourseRoleListItem>();

  const { data, isFetching, error } = useGetListCourseRolesQuery({
    courseId,
  });

  const {
    data: coursePermissions,
    isFetching: isFetchingCoursePermissions,
    error: coursePermissionsError,
  } = useGetListCoursePermissionsQuery();

  if (error || coursePermissionsError) {
    return <NavigateToError error={error ?? coursePermissionsError} />;
  }

  if (!isFetching && !data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <CourseRolesHead />
      <CourseRolesBreadcrumb courseId={courseId} />
      <CourseRoleCreationButton
        isLoading={isFetchingCoursePermissions}
        updateModalMode={setModalMode}
      />
      <CourseRolesTable
        isLoading={isFetching || isFetchingCoursePermissions}
        courseRoles={data}
        updateModalMode={setModalMode}
        updateCurrentCourseRole={setCurrentCourseRole}
      />
      <CourseRolesModals
        courseId={courseId ?? ''}
        modalMode={modalMode}
        permissions={coursePermissions ?? []}
        currentCourseRole={currentCourseRole}
        onCloseModal={() => setModalMode(undefined)}
      />
    </>
  );
}
