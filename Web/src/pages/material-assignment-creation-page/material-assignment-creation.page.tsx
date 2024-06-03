import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialAssignmentCreationBreadcrumb from './material-assignment-creation.breadcrumb';
import MaterialAssignmentCreationForm from './material-assignment-creation.form';
import MaterialAssignmentCreationHead from './material-assignment-creation.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialAssignmentCreationPage() {
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
      <MaterialAssignmentCreationHead />
      <MaterialAssignmentCreationBreadcrumb
        courseId={data.courseId}
        courseTitle={data.title}
      />
      <MaterialAssignmentCreationForm
        courseId={data.courseId}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
      />
    </>
  );
}
