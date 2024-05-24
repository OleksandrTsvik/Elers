import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialFileCreationBreadcrumb from './material-file-creation.breadcrumb';
import MaterialFileCreationForm from './material-file-creation.form';
import MaterialFileCreationHead from './material-file-creation.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToNotFound } from '../../common/navigate';

export default function MaterialFileCreationPage() {
  const { tabId } = useParams();
  const { data, isLoading } = useGetCourseByTabIdQuery({ id: tabId });

  if (isLoading) {
    return <Skeleton active />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MaterialFileCreationHead />
      <MaterialFileCreationBreadcrumb
        courseId={data.courseId}
        title={data.title}
      />
      <MaterialFileCreationForm
        courseId={data.courseId}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
      />
    </>
  );
}
