import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import CourseChangeImageBreadcrumb from './course-change-image.breadcrumb';
import CourseChangeImageHead from './course-change-image.head';
import CourseChangeImageWidget from './course-change-image.widget';
import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToNotFound } from '../../common/navigate';

export default function CourseChangeImagePage() {
  const { courseId } = useParams();
  const { data, isFetching } = useGetCourseByIdQuery({ id: courseId });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <CourseChangeImageHead />
      <CourseChangeImageBreadcrumb courseId={data.id} title={data.title} />
      <CourseChangeImageWidget courseId={data.id} />
    </>
  );
}
