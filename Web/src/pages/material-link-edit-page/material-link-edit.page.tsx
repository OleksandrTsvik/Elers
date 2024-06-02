import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialLinkEditBreadcrumb from './material-link-edit.breadcrumb';
import MaterialLinkEditForm from './material-link-edit.form';
import MaterialLinkEditHead from './material-link-edit.head';
import { useGetCourseMaterialLinkQuery } from '../../api/course-materials.queries.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialLinkEditPage() {
  const { tabId, id } = useParams();

  const { data, isFetching, error } = useGetCourseMaterialLinkQuery({
    tabId,
    id,
  });

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
      <MaterialLinkEditHead />
      <MaterialLinkEditBreadcrumb
        courseId={data.courseId}
        title={data.courseTitle}
      />
      <MaterialLinkEditForm
        courseId={data.courseId}
        courseMaterialId={data.id}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
        title={data.title}
        link={data.link}
      />
    </>
  );
}
