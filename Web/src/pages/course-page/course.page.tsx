import { Skeleton } from 'antd';
import { useParams } from 'react-router-dom';

import CourseContent from './course.content';
import CourseHead from './course.head';
import CourseHeader from './course.header';
import { useGetCourseByIdQuery } from '../../api/courses.api';
import { NavigateToNotFound } from '../../common/navigate';

export default function CoursePage() {
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
      <CourseHead title={data.title} />
      <CourseHeader
        courseId={data.id}
        title={data.title}
        description={data.description}
      />
      <CourseContent course={data} />
    </>
  );
}
