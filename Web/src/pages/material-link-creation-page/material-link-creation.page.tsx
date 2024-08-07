import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialLinkCreationBreadcrumb from './material-link-creation.breadcrumb';
import MaterialLinkCreationForm from './material-link-creation.form';
import MaterialLinkCreationHead from './material-link-creation.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialLinkCreationPage() {
  const { tabId } = useParams();
  const { data, isLoading, error } = useGetCourseByTabIdQuery({ id: tabId });

  if (isLoading) {
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
      <MaterialLinkCreationHead />
      <MaterialLinkCreationBreadcrumb
        courseId={data.courseId}
        title={data.title}
      />
      <MaterialLinkCreationForm
        courseId={data.courseId}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
      />
    </>
  );
}
