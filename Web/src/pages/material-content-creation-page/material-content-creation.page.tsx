import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialContentCreationBreadcrumb from './material-content-creation.breadcrumb';
import MaterialContentCreationEditor from './material-content-creation.editor';
import MaterialContentCreationHead from './material-content-creation.head';
import { useGetCourseByTabIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialContentCreationPage() {
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
      <MaterialContentCreationHead />
      <MaterialContentCreationBreadcrumb
        courseId={data.courseId}
        title={data.title}
      />
      <MaterialContentCreationEditor
        courseId={data.courseId}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
      />
    </>
  );
}
