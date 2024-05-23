import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import MaterialContentEditBreadcrumb from './material-content-edit.breadcrumb';
import MaterialContentEditEditor from './material-content-edit.editor';
import MaterialContentEditHead from './material-content-edit.head';
import { useGetCourseMaterialContentQuery } from '../../api/course-materials.api';
import { NavigateToNotFound } from '../../common/navigate';

export default function MaterialContentEditPage() {
  const { tabId, id } = useParams();

  const { data, isFetching } = useGetCourseMaterialContentQuery({ tabId, id });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <MaterialContentEditHead />
      <MaterialContentEditBreadcrumb
        courseId={data.courseId}
        title={data.courseTitle}
      />
      <MaterialContentEditEditor
        courseId={data.courseId}
        courseMaterialId={data.id}
        tabId={data.tabId}
        courseTabType={data.courseTabType}
        content={data.content}
      />
    </>
  );
}
