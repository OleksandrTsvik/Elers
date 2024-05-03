import { useParams } from 'react-router-dom';

import CourseContent from './course.content';
import { NavigateToNotFound } from '../../shared';

export default function CoursePage() {
  const { courseId } = useParams();

  if (!courseId) {
    return <NavigateToNotFound />;
  }

  return <CourseContent courseId={courseId} />;
}
