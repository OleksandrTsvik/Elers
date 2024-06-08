import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialTestEditBreadcrumb from './material-test-edit.breadcrumb';
import MaterialTestEditForm from './material-test-edit.form';
import MaterialTestEditHead from './material-test-edit.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialTestEditPage() {
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
      <MaterialTestEditHead />
      <MaterialTestEditBreadcrumb
        courseId={data.courseId}
        courseTitle={data.title}
      />
      <MaterialTestEditForm />
    </>
  );
}
