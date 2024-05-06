import { Skeleton } from 'antd';
import { useEffect } from 'react';
import { useParams } from 'react-router-dom';

import CourseEditPageContent from './course-edit.page-content';
import { coursesApi, useGetCourseByIdToEditQuery } from '../../api/courses.api';
import { useAppDispatch } from '../../hooks/redux-hooks';
import { NavigateToNotFound } from '../../shared';

export default function CourseEditPage() {
  const appDispatch = useAppDispatch();

  const { courseId } = useParams();
  const { data, isFetching } = useGetCourseByIdToEditQuery({ id: courseId });

  useEffect(() => {
    return () => {
      appDispatch(coursesApi.util.invalidateTags(['CourseToEdit']));
    };
  }, [appDispatch]);

  if (isFetching) {
    return <Skeleton />;
  }

  if (!data) {
    return <NavigateToNotFound />;
  }

  return <CourseEditPageContent course={data} />;
}
