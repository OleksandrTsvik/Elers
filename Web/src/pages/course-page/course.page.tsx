import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import CourseTabs from './course.tabs';
import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToError, NavigateToNotFound } from '../../common/navigate';

export default function CoursePage() {
  const { courseId } = useParams();
  const { data, isFetching, error } = useGetCourseByIdQuery({ id: courseId });

  if (isFetching) {
    return <Skeleton active />;
  }

  if (error) {
    return <NavigateToError error={error} />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return <CourseTabs course={data} />;
}
