import { Skeleton } from 'antd';

import CourseHead from './course.head';
import CourseHeader from './course.header';
import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToNotFound } from '../../shared';

interface Props {
  courseId: string;
}

export default function CourseContent({ courseId }: Props) {
  const { data, isFetching } = useGetCourseByIdQuery({ id: courseId });

  if (isFetching) {
    return <Skeleton />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return (
    <>
      <CourseHead title={data.title} />
      <CourseHeader title={data.title} description={data.description} />
    </>
  );
}
