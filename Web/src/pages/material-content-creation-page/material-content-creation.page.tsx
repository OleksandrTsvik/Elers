import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialContentCreationBreadcrumb from './material-content-creation.breadcrumb';
import MaterialContentCreationEditor from './material-content-creation.editor';
import MaterialContentCreationHead from './material-content-creation.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToNotFound } from '../../common/navigate';

export default function MaterialContentCreationPage() {
  const { tabId } = useParams();
  const { data, isLoading } = useGetCourseByTabIdQuery({ id: tabId });

  if (isLoading) {
    return <Skeleton />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MaterialContentCreationHead />
      <MaterialContentCreationBreadcrumb
        courseId={data.courseId ?? ''}
        title={data.title ?? ''}
      />
      <MaterialContentCreationEditor tabId={data.tabId} />
    </>
  );
}
