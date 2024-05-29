import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import CourseChangeImageBreadcrumb from './course-change-image.breadcrumb';
import CourseChangeImageHead from './course-change-image.head';
import CourseChangeImageWidget from './course-change-image.widget';
import CourseDeleteImageButton from './course-delete-image.button';
import { useGetCourseByIdToEditQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function CourseChangeImagePage() {
  const { courseId } = useParams();

  const { data, isFetching, error } = useGetCourseByIdToEditQuery({
    id: courseId,
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
      <CourseChangeImageHead />
      <CourseChangeImageBreadcrumb courseId={data.id} title={data.title} />
      <CourseDeleteImageButton courseId={data.id} disabled={!data.imageUrl} />
      <CourseChangeImageWidget courseId={data.id} />
    </>
  );
}
