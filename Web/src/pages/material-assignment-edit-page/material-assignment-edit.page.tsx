import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialAssignmentEditBreadcrumb from './material-assignment-edit.breadcrumb';
import MaterialAssignmentEditForm from './material-assignment-edit.form';
import MaterialAssignmentEditHead from './material-assignment-edit.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialAssignmentEditPage() {
  const { tabId } = useParams();

  const { data, isFetching, error } = useGetCourseByTabIdQuery({ id: tabId });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MaterialAssignmentEditHead />
      <MaterialAssignmentEditBreadcrumb
        courseId={data.courseId}
        courseTitle={data.title}
      />
      <MaterialAssignmentEditForm
        courseId={data.courseId}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
      />
    </>
  );
}
