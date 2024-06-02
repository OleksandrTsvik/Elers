import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialFileEditBreadcrumb from './material-file-edit.breadcrumb';
import MaterialFileEditForm from './material-file-edit.form';
import MaterialFileEditHead from './material-file-edit.head';
import { useGetCourseMaterialFileQuery } from '../../api/course-materials.queries.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function MaterialFileEditPage() {
  const { tabId, id } = useParams();
  const { data, isFetching, error } = useGetCourseMaterialFileQuery({
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
      <MaterialFileEditHead />
      <MaterialFileEditBreadcrumb
        courseId={data.courseId}
        title={data.courseTitle}
      />
      <MaterialFileEditForm
        courseId={data.courseId}
        courseMaterialId={data.id}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
        fileTitle={data.fileTitle}
      />
    </>
  );
}